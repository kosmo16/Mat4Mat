using Components;
using Content;
using Framework.Events;
using UnityEngine;

namespace Systems
{
    public class PlayerControlSystem : Framework.Core.System
    {
        private Event0 playerLose;
        private Event0 needClue;

        public Rect leftArrowRectangle;
        public Rect rightArrowRectangle;
        public Rect upArrowRectangle;
        public Rect repeatLevelRectangle;
        public Rect clueRectangle;
        public Texture2D leftArrowTexture;
        public Texture2D rightArrowTexture;
        public Texture2D upArrowTexture;
        public Texture2D repeatLevelTexture;
        public Texture2D clueTexture;
        public Transform groundCheck;
	    public float jumpCooldown;

        private bool facingRight = true;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isGrounded = false;
        private bool isJumping = false;
        private float previousYPosition = 0.0f;
        private float currentJumpCooldown;

        public override void Initialize()
        {
            playerLose = GetEvent(Event.PlayerLose);
            needClue = GetEvent(Event.NeedClue);

            Player player = GetFirstOrNull<Player>();
            previousYPosition = player.transform.position.y;
        }

        public override void OnUpdate()
        {
            if (currentJumpCooldown >= 0.0f)
            {
                currentJumpCooldown -= DeltaTime;
            }

            Player player = GetFirstOrNull<Player>();
            //Jump(player.rigidbody, player.behaviour);
            Move(player);

            if (isMovingRight || isMovingLeft)
            {
                if (!player.animator.GetBool("MoveRight"))
                {
                    player.animator.SetBool("MoveRight", true);
                    player.animator.CrossFade("mat_move_right", 0.1f);

                    if ((isMovingLeft && facingRight)
                        || (isMovingRight && !facingRight))
                    {
                        Vector3 localScale = player.transform.localScale;
                        localScale.x *= -1;
                        player.transform.localScale = localScale;

                        if (isMovingRight)
                        {
                            facingRight = true;
                        }
                        else
                        {
                            facingRight = false;
                        }
                    }
                }
            }
            else
            {
                if (player.animator.GetBool("MoveRight"))
                {
                    player.animator.SetBool("MoveRight", false);
                    player.animator.CrossFade("mat_idle_right", 0.1f);
                }
            }
        }

        public void OnGUI()
        {
            Player player = GetFirstOrNull<Player>();
            Rigidbody2D rigidbody = player.rigidbody;
            PhysicsBehaviour behaviour = player.behaviour;
            GUI.backgroundColor = Color.clear;

            MoveLeft(behaviour, rigidbody);
            MoveRight(behaviour, rigidbody);
            Jump(player.rigidbody, player.behaviour);
            RepeatLevel();
            Clue();
        }

        private void Clue()
        {
            if (GUI.Button(clueRectangle, clueTexture))
            {
                needClue.Report();
            }
        }

        private void RepeatLevel()
        {
            if (GUI.Button(repeatLevelRectangle, repeatLevelTexture))
            {
                playerLose.Report();
            }
        }

        private void MoveLeft(PhysicsBehaviour behaviour, Rigidbody2D rigidbody)
        {
            Rect reversedLeftArrowRectangle = new Rect(
            leftArrowRectangle.xMin,
            Screen.height - leftArrowRectangle.yMin - leftArrowRectangle.height,
            leftArrowRectangle.width,
            leftArrowRectangle.height);

            GUI.Button(reversedLeftArrowRectangle, leftArrowTexture);
            isMovingLeft = false;

            foreach (Touch touch in Input.touches)
            {
                if (leftArrowRectangle.Contains(touch.position))
                {
                    isMovingLeft = true;
                    rigidbody.AddForce(-Vector2.right * behaviour.moveForce);
                    break;
                }
            }

            if (Mathf.Abs(rigidbody.velocity.x) > behaviour.maxSpeed)
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Sign(rigidbody.velocity.x) * behaviour.maxSpeed, rigidbody.velocity.y);
            }
        }

        private void MoveRight(PhysicsBehaviour behaviour, Rigidbody2D rigidbody)
        {
            Player player = GetFirstOrNull<Player>();

            Rect reversedRightArrowRectangle = new Rect(
                rightArrowRectangle.xMin,
                Screen.height - rightArrowRectangle.yMin - rightArrowRectangle.height,
                rightArrowRectangle.width,
                rightArrowRectangle.height);

            GUI.Button(reversedRightArrowRectangle, rightArrowTexture);
            isMovingRight = false;

            foreach (Touch touch in Input.touches)
            {
                if (rightArrowRectangle.Contains(touch.position))
                {
                    isMovingRight = true;
                    rigidbody.AddForce(Vector2.right * behaviour.moveForce);
                    break;
                }
            }


            if (Mathf.Abs(rigidbody.velocity.x) > behaviour.maxSpeed)
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Sign(rigidbody.velocity.x) * behaviour.maxSpeed, rigidbody.velocity.y);
            }
        }

        private void Jump(Rigidbody2D rigidbody, PhysicsBehaviour behaviour)
        {
            Rect reversedUpArrowRectangle = new Rect(
                Screen.width - upArrowRectangle.xMin - upArrowRectangle.width,
                Screen.height - upArrowRectangle.yMin - upArrowRectangle.height,
                upArrowRectangle.width,
                upArrowRectangle.height);

            Rect mirrowRectangle = new Rect(
                reversedUpArrowRectangle.xMin,
                upArrowRectangle.yMin,
                upArrowRectangle.width,
                upArrowRectangle.height);

            isGrounded = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, 0.01f, 1 << LayerMask.NameToLayer("Ground")).collider != null;
            GUI.Button(reversedUpArrowRectangle, upArrowTexture);

            foreach (Touch touch in Input.touches)
            {
                if (mirrowRectangle.Contains(touch.position) 
                    && isGrounded
                    && currentJumpCooldown <= 0.0f
                    && previousYPosition == rigidbody.transform.position.y)
                {
                    rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
                    currentJumpCooldown = jumpCooldown;
                    break;
                }
            }

            previousYPosition = rigidbody.transform.position.y;
        }

        private void Move(Player player)
        {
            Rigidbody2D rigidbody = player.rigidbody;
            PhysicsBehaviour behaviour = player.behaviour;
			Debug.Log (player.behaviour.name);
            float h = Input.GetAxis("Horizontal");

            rigidbody.AddForce(Vector2.right * h * behaviour.moveForce);
            if (h > 0.0f)
            {
                player.facingRight = true;

            }
            else if (h < 0.0f)
            {
                player.facingRight = false;
            }

            if (Mathf.Abs(rigidbody.velocity.x) > behaviour.maxSpeed)
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Sign(rigidbody.velocity.x) * behaviour.maxSpeed, rigidbody.velocity.y);
            }
        }
    }
}

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
        public Rect specialRectangle;
        public Texture2D leftArrowTexture;
        public Texture2D rightArrowTexture;
        public Texture2D upArrowTexture;
        public Texture2D repeatLevelTexture;
        public Texture2D clueTexture;
        public Texture2D destroyTexture;
        public Texture2D dashTexture;
        public Transform groundCheck;
	    public float jumpCooldown;
        public float punchDistance = 1.0f;
        public float speed = 2.0f;
        public float punchCooldown = 0.2f;
        public float beforeCooldown = 0.25f;

        private Vector3 positionAfterPunch;
        private bool facingRight = true;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isGrounded = false;
        private bool isJumping = false;
        private float previousYPosition;
        private float currentPunchCooldown;
        private float currentBeforeCooldown;
        private float currentJumpCooldown;
        

        public override void Initialize()
        {
            playerLose = GetEvent(Event.PlayerLose);
            needClue = GetEvent(Event.NeedClue);

            Player player = GetFirstOrNull<Player>();
            player.isActive = true;
            previousYPosition = player.transform.position.y;
        }

        public override void OnUpdate()
        {
            Player player = GetFirstOrNull<Player>();

            player.animator.SetFloat("Velocity", player.rigidbody.velocity.y);


            if (punchCooldown > 0.0f)
            {
                currentPunchCooldown -= DeltaTime;
                currentPunchCooldown = Mathf.Clamp(currentPunchCooldown, 0.0f, punchCooldown);  
            }

            currentBeforeCooldown -= DeltaTime;
            currentBeforeCooldown = Mathf.Clamp(currentBeforeCooldown, 0.0f, beforeCooldown);
 

            if (!player.isActive
                && currentBeforeCooldown <= 0.0f)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, positionAfterPunch, DeltaTime * speed);

                if (player.transform.position == positionAfterPunch)
                {
                    player.isActive = true;

                    if (player.behaviour.canDash)
                    {
                        player.animator.SetBool("Dash", false);
                        player.dashObject.collider2D.enabled = true;
                    }
                    else
                    {
                        player.animator.SetBool("Punch", false);
                    }

                    currentPunchCooldown = punchCooldown;
                }
            }

            if (currentPunchCooldown == 0.0f && player.isActive && !player.animator.GetBool("Move") && Mathf.Abs(player.rigidbody.velocity.x) > 0.0f)
            {
                player.animator.SetBool("Move", true);
            }
            else if (!player.isActive || (player.animator.GetBool("Move") && player.rigidbody.velocity.x == 0.0f))
            {
                player.animator.SetBool("Move", false);
            }
        }

        public override void OnFixedUpdate()
        {
            Player player = GetFirstOrNull<Player>();

            if (currentJumpCooldown >= 0.0f)
            {
                currentJumpCooldown -= DeltaTime;
            }

            Move(player);
        }

        public void OnGUI()
        {
            Player player = GetFirstOrNull<Player>();
            Rigidbody2D rigidbody = player.rigidbody;
            PhysicsBehaviour behaviour = player.behaviour;
            GUI.backgroundColor = Color.clear;

            DestroyingObject(player);
            DashingObject(player);
            MoveLeft(behaviour, rigidbody);
            MoveRight(behaviour, rigidbody);
            Jump(player.rigidbody, player.behaviour);
            RepeatLevel();
            Clue();
        }

        private void DashingObject(Player player)
        {
            Rect reversedSpecialRectangle = new Rect(
              Screen.width - specialRectangle.xMin - specialRectangle.width,
              Screen.height - specialRectangle.yMin - specialRectangle.height,
              specialRectangle.width,
              specialRectangle.height);

            Rect mirrowRectangle = new Rect(
               reversedSpecialRectangle.xMin,
               specialRectangle.yMin,
               specialRectangle.width,
               specialRectangle.height);


            if (player.behaviour.canDash && player.dashObject != null)
            {
                GUI.Button(reversedSpecialRectangle, dashTexture);

                if (player.isActive)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (mirrowRectangle.Contains(touch.position))
                        {
                            player.animator.SetBool("Dash", true);
                            player.isActive = false;
                            Vector3 target = player.transform.position;
                            target.x += Mathf.Sign(player.destroyingObject.transform.position.x - player.transform.position.x) * punchDistance;
                            positionAfterPunch = player.transform.position;
                            positionAfterPunch = target;
                            player.dashObject.collider2D.enabled = false;
                            currentBeforeCooldown = beforeCooldown;
                            break;
                        }
                    }

                    //if (Input.GetButton("Fire1"))
                    //{
                    //    player.animator.SetBool("Dash", true);
                    //    player.isActive = false;
                    //    Vector3 target = player.transform.position;
                    //    target.x += Mathf.Sign(player.destroyingObject.transform.position.x - player.transform.position.x) * punchDistance;
                    //    positionAfterPunch = player.transform.position;
                    //    positionAfterPunch = target;
                    //    player.destroyingObject.collider2D.enabled = false;
                    //    currentBeforeCooldown = beforeCooldown;
                    //}
                }
            }
        }

        private void DestroyingObject(Player player)
        {
            Rect reversedSpecialRectangle = new Rect(
               Screen.width - specialRectangle.xMin - specialRectangle.width,
               Screen.height - specialRectangle.yMin - specialRectangle.height,
               specialRectangle.width,
               specialRectangle.height);

            Rect mirrowRectangle = new Rect(
               reversedSpecialRectangle.xMin,
               specialRectangle.yMin,
               specialRectangle.width,
               specialRectangle.height);


            if (player.behaviour.canDestroyObstacles && player.destroyingObject != null)
            {
                GUI.Button(reversedSpecialRectangle, destroyTexture);

                if (player.isActive)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (mirrowRectangle.Contains(touch.position))
                        {
                            player.destroyingObject.animator.SetBool("Destroyed", true);
                            player.animator.SetBool("Punch", true);
                            player.isActive = false;
                            Vector3 target = player.transform.position;
                            float offset = player.destroyingObject.transform.position.x - player.transform.position.x;
                            target.x += offset + Mathf.Sign(offset) * 0.55f;
                            positionAfterPunch = player.transform.position;
                            positionAfterPunch = target;
                            player.destroyingObject.collider2D.enabled = false;
                            GameObject.Destroy(player.destroyingObject.transform.gameObject, 1.0f);
                            currentBeforeCooldown = beforeCooldown;
                            break;
                        }
                    }

                    //if (Input.GetButton("Fire1"))
                    //{
                    //    player.destroyingObject.animator.SetBool("Destroyed", true);
                    //    player.animator.SetBool("Punch", true);
                    //    player.isActive = false;
                    //    Vector3 target = player.transform.position;
                    //    target.x += Mathf.Sign(player.destroyingObject.transform.position.x - player.transform.position.x) * punchDistance;
                    //    positionAfterPunch = player.transform.position;
                    //    positionAfterPunch = target;
                    //    player.destroyingObject.collider2D.enabled = false;
                    //    GameObject.Destroy(player.destroyingObject.transform.gameObject, 1.0f);
                    //    currentBeforeCooldown = beforeCooldown;
                    //}
                }
            }
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
            Player player = GetFirstOrNull<Player>();

            Rect reversedLeftArrowRectangle = new Rect(
            leftArrowRectangle.xMin,
            Screen.height - leftArrowRectangle.yMin - leftArrowRectangle.height,
            leftArrowRectangle.width,
            leftArrowRectangle.height);

            GUI.Button(reversedLeftArrowRectangle, leftArrowTexture);

            if (player.isActive)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (leftArrowRectangle.Contains(touch.position))
                    {
                        if (player.transform.localScale.x > 0.0f)
                        {
                            Vector3 localScale = player.transform.localScale;
                            localScale.x *= -1;
                            player.transform.localScale = localScale;
                            break;
                        }

                        rigidbody.AddForce(-Vector2.right * behaviour.moveForce);
                        break;
                    }
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

            if (player.isActive)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (rightArrowRectangle.Contains(touch.position))
                    {
                        if (player.transform.localScale.x < 0.0f)
                        {
                            Vector3 localScale = player.transform.localScale;
                            localScale.x *= -1;
                            player.transform.localScale = localScale;
                            break;
                        }

                        rigidbody.AddForce(Vector2.right * behaviour.moveForce);
                        break;
                    }
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
            Player player = GetFirstOrNull<Player>();

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

            isGrounded = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, 0.02f, 1 << LayerMask.NameToLayer("Ground")).collider != null;

            if (isGrounded)
            {
                player.animator.SetBool("Jump", false);
            }
            else
            {
                player.animator.SetBool("Jump", true);
            }

            GUI.Button(reversedUpArrowRectangle, upArrowTexture);

            if (player.isActive)
            {

                foreach (Touch touch in Input.touches)
                {
                    if (mirrowRectangle.Contains(touch.position)
                        && isGrounded
                        && currentJumpCooldown <= 0.0f
                        && previousYPosition == rigidbody.transform.position.y)
                    {
                        player.animator.SetBool("Jump", true);
                        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, 0.1f * behaviour.maxSpeed);
                        rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
                        currentJumpCooldown = jumpCooldown;
                        break;
                    }
                }
            }

            if (Input.GetButtonDown("Jump")
                    && isGrounded
                    && currentJumpCooldown <= 0.0f
                    && previousYPosition == rigidbody.transform.position.y)
            {
                    player.animator.SetBool("Jump", true);
                    rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, 0.1f * behaviour.maxSpeed);
                    rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
                    currentJumpCooldown = jumpCooldown;
            }

            previousYPosition = rigidbody.transform.position.y;
        }

        private void Move(Player player)
        {
            Rigidbody2D rigidbody = player.rigidbody;
            PhysicsBehaviour behaviour = player.behaviour;
			Debug.Log (player.behaviour.name);
            float h = Input.GetAxis("Horizontal");
            isMovingLeft = false;
            isMovingRight = false;

            rigidbody.AddForce(Vector2.right * h * behaviour.moveForce);
            if (h > 0.0f)
            {
                player.facingRight = true;
                isMovingRight = true;

            }
            else if (h < 0.0f)
            {
                player.facingRight = false;
                isMovingLeft = true;
            }

            if (Mathf.Abs(rigidbody.velocity.x) > behaviour.maxSpeed)
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Sign(rigidbody.velocity.x) * behaviour.maxSpeed, rigidbody.velocity.y);
            }
        }
    }
}

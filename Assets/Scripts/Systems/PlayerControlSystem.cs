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

        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isGrounded = false;
        private bool isJumping = false;

        public override void Initialize()
        {
            playerLose = GetEvent(Event.PlayerLose);
            needClue = GetEvent(Event.NeedClue);
        }

        public override void OnUpdate()
        {
            Player player = GetFirstOrNull<Player>();
            //Jump(player.rigidbody, player.behaviour);
            Move(player);

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
            if (GUI.Button(repeatLevelRectangle, repeatLevelTexture) )
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

            UnityEngine.Event e = UnityEngine.Event.current;

            if ((e.type == EventType.MouseDown) && reversedLeftArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isMovingLeft = true;
            }

            if (GUI.Button(reversedLeftArrowRectangle, leftArrowTexture) || !reversedLeftArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isMovingLeft = false;
            }

            if (isMovingLeft)
            {
                rigidbody.AddForce(-Vector2.right * behaviour.moveForce);
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

            UnityEngine.Event e = UnityEngine.Event.current;

            if ((e.type == EventType.MouseDown) && reversedRightArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isMovingRight = true;
            }

            if (GUI.Button(reversedRightArrowRectangle, rightArrowTexture) || !reversedRightArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isMovingRight = false;
            }

            if (isMovingRight)
            {
                rigidbody.AddForce(Vector2.right * behaviour.moveForce);
            }
			else
			{

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

			isGrounded = true;//Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));


            UnityEngine.Event e = UnityEngine.Event.current;

            if ((e.type == EventType.MouseDown) && reversedUpArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isJumping = true;
            }

            if (GUI.Button(reversedUpArrowRectangle, upArrowTexture) || !reversedUpArrowRectangle.Contains(UnityEngine.Event.current.mousePosition))
            {
                isJumping = false;
            }

            if (isJumping && isGrounded)
            {
                rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
                isJumping = false;
            }
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

using Components;
using Content;
using UnityEngine;

namespace Systems
{
    public class PlayerControlSystem : Framework.Core.System
    {
        public Rect leftArrowRectangle;
        public Rect rightArrowRectangle;
        public Rect upArrowRectangle;
        public Texture2D leftArrowTexture;
        public Texture2D rightArrowTexture;
        public Texture2D upArrowTexture;

        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        
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
        }

        private void MoveLeft(PhysicsBehaviour behaviour, Rigidbody2D rigidbody)
        {
            Rect reversedLeftArrowRectangle = new Rect(
            leftArrowRectangle.xMin,
            Screen.height - leftArrowRectangle.yMin - leftArrowRectangle.height,
            leftArrowRectangle.width,
            leftArrowRectangle.height);

            Event e = Event.current;

            if ((e.type == EventType.MouseDown) && reversedLeftArrowRectangle.Contains(Event.current.mousePosition))
            {
                isMovingLeft = true;
            }

            if (GUI.Button(reversedLeftArrowRectangle, leftArrowTexture))
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
            Rect reversedRightArrowRectangle = new Rect(
                rightArrowRectangle.xMin,
                Screen.height - rightArrowRectangle.yMin - rightArrowRectangle.height,
                rightArrowRectangle.width,
                rightArrowRectangle.height);

            Event e = Event.current;

            if ((e.type == EventType.MouseDown) && reversedRightArrowRectangle.Contains(Event.current.mousePosition))
            {
                isMovingRight = true;
            }

            if (GUI.Button(reversedRightArrowRectangle, rightArrowTexture))
            {
                isMovingRight = false;
            }

            if (isMovingRight)
            {
                rigidbody.AddForce(Vector2.right * behaviour.moveForce);
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

            if (GUI.Button(reversedUpArrowRectangle, upArrowTexture))
            {
                rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
            }

            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
            }
        }

        private void Move(Player player)
        {
            Rigidbody2D rigidbody = player.rigidbody;
            PhysicsBehaviour behaviour = player.behaviour;
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

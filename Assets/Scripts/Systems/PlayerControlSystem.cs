using Components;
using Content;
using UnityEngine;

namespace Systems
{
    public class PlayerControlSystem : Framework.Core.System
    {
        public override void OnFixedUpdate()
        {
            Player player = GetFirstOrNull<Player>();
            Move(player);
            Jump(player.rigidbody, player.behaviour);
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

        private void Jump(Rigidbody2D rigidbody, PhysicsBehaviour behaviour)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(new Vector2(0f, behaviour.jumpForce));
            }
        }
    }
}

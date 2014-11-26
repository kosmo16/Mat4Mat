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
            Move(player.rigidbody, player.behaviour);
            Jump(player.rigidbody, player.behaviour);
        }

        private void Move(Rigidbody2D rigidbody, PhysicsBehaviour behaviour)
        {
            float h = Input.GetAxis("Horizontal");
            rigidbody.AddForce(Vector2.right * h * behaviour.moveForce);

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

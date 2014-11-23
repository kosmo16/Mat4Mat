using Components;
using Content;
using UnityEngine;

namespace Systems
{
    public class PlayerControlSystem : Framework.Core.System
    {
        public override void OnUpdate()
        {
            Player player = GetFirstOrNull<Player>();
            Move(player.rigidbody, player.behaviours[player.currentBehaviour]);
            Jump(player.rigidbody, player.behaviours[player.currentBehaviour]);
            NextBehaviour(player);
        }

        private void NextBehaviour(Player player)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                player.currentBehaviour = (player.currentBehaviour + 1) % player.behaviours.Length;
                player.rigidbody.mass = player.behaviours[player.currentBehaviour].mass;
            }
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

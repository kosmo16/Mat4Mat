using Components;
using Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class EnemySystem : Framework.Core.System
    {
        public Event0 playerLose; 

        public override void Initialize()
        {
            playerLose = GetEvent(Event.PlayerLose);
        }

        public override void OnUpdate()
        {
            foreach (Enemy enemy in GetListOf<Enemy>())
            {
                if (enemy.renderer.isVisible)
                {
                    if (enemy.rigidbody.velocity == Vector2.zero)
                    {
                        enemy.rigidbody.velocity = enemy.speed * -Vector2.right;
                    }
                    else
                    {
                        enemy.rigidbody.velocity = enemy.speed * enemy.rigidbody.velocity.normalized;
                    }
                }

                if (enemy.collisionWithPlayer)
                {
                    playerLose.Report();
                }
            }
        }
    }
}

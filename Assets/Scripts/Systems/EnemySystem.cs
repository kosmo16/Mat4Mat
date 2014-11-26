using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class EnemySystem : Framework.Core.System
    {
        public override void OnFixedUpdate()
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
            }
        }
    }
}

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
                Debug.Log(enemy.rigidbody.velocity);
                enemy.rigidbody.velocity = enemy.speed * enemy.rigidbody.velocity.normalized;
            }
        }
    }
}

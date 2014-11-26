using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class ThunderSpawningSystem : Framework.Core.System
    {
        public override void OnUpdate()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Player player = GetFirstOrNull<Player>();

                if (player.behaviour.canSpawnThunders)
                {
                    if (player.facingRight)
                    {
                        Thunder thunder = Instantiate(player.thunderSpawner.thunderPrefab, player.thunderSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f))) as Thunder;
                        thunder.rigidbody.velocity = new Vector2(thunder.speed, 0);
                    }
                    else
                    {
                        Thunder thunder = Instantiate(player.thunderSpawner.thunderPrefab, player.thunderSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, -90f))) as Thunder;
                        thunder.rigidbody.velocity = new Vector2(-thunder.speed, 0);
                    }
                }
            }
        }
    }
}

﻿using Components;
using Framework.Events;
using UnityEngine;

namespace Systems
{
    public class GatherCollectibleSystem : Framework.Core.System
    {
        private Event0 gatherCoin;

        public override void Initialize()
        {
            gatherCoin = GetEvent(Event.GatherCoin);
        }

        public override void OnUpdate()
        {
            foreach (Collectible collectible in GetListOf<Collectible>())
            {
                if (collectible.collisionWithPlayer)
                {
                    Player player = GetFirstOrNull<Player>();

                    if (collectible.type == CollectibleType.Transform)
                    {
                        player.behaviour = collectible.behaviour;
                        player.rigidbody.mass = player.behaviour.mass;
                        player.rigidbody.Sleep();

                        if (player.behaviour.canDestroyObstacles)
                        {
                            player.animator.SetBool("Stone", true);
                            player.animator.SetBool("Default", false);
                            player.animator.SetBool("Paper", false);
                        }
                        else
                        {
                            player.animator.SetBool("Stone", false);
                            player.animator.SetBool("Default", true);
                            player.animator.SetBool("Paper", false);
                        }
                    }
                    else if (collectible.type == CollectibleType.Coin)
                    {
                        player.score++;
                        gatherCoin.Report();

                    }

                    GameObject.Destroy(collectible.gameObject);
                }
            }
        }
    }
}

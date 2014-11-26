using Components;
using UnityEngine;

namespace Systems
{
    public class GatherCollectibleSystem : Framework.Core.System
    {
        public override void OnFixedUpdate()
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
                    }
                    else if (collectible.type == CollectibleType.Coin)
                    {
                        player.score++;
                    }

                    GameObject.Destroy(collectible.gameObject);
                }
            }
        }
    }
}

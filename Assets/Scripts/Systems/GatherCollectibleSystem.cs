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
                    GameObject.Destroy(collectible.gameObject);
                }
            }
        }
    }
}

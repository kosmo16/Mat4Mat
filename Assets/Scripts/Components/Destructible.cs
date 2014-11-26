using Content;
using Framework.Core;
using UnityEngine;

namespace Components
{
    public class Destructible : SystemComponent
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PhysicsBehaviour behaviour = collision.gameObject.GetComponent<Player>().behaviour;

                if (behaviour.canDestroyObstacles)
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}

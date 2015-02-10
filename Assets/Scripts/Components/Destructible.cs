using Content;
using Framework.Core;
using UnityEngine;

namespace Components
{
    public class Destructible : SystemComponent
    {
		public Animator animator;
		public Collider2D collider;
		public float destroyTime;


        public void OnTriggerEnter2D(Collider2D collision)
        {
			if (collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();
                PhysicsBehaviour behaviour = player.behaviour;

                if (behaviour.canDestroyObstacles)
                {
                    player.destroyingObject = this;
                }
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            PhysicsBehaviour behaviour = player.behaviour;

            if (behaviour.canDestroyObstacles)
            {
                player.destroyingObject = null;
            }
        }
    }
}
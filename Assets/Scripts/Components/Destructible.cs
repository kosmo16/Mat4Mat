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

        public void OnCollisionEnter2D(Collision2D collision)
        {
			if (collision.gameObject.tag == "Player")
            {
                PhysicsBehaviour behaviour = collision.gameObject.GetComponent<Player>().behaviour;

                if (behaviour.canDestroyObstacles)
                {
					animator.SetBool("Destroyed", true);
					Physics2D.IgnoreCollision(collision.collider, collider);
					GameObject.Destroy(transform.parent.gameObject, destroyTime);
                }
            }
        }

		public void OnCollisionStay2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				PhysicsBehaviour behaviour = collision.gameObject.GetComponent<Player>().behaviour;
				
				if (behaviour.canDestroyObstacles)
				{
					Physics2D.IgnoreCollision(collision.collider, collider);
				}
			}
		}
    }
}
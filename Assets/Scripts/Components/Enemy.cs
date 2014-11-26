using Framework.Core;
using UnityEngine;

namespace Components
{
    public class Enemy : SystemComponent
    {
        public Rigidbody2D rigidbody;
        public float speed;
        public bool collisionWithObstacle;

        public void Start()
        {
            rigidbody.velocity = new Vector2(-speed, 0.0f);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Obstacles")
            {
                foreach (ContactPoint2D point in collision.contacts)
                {
                    Vector2 vector = point.point - new Vector2(transform.position.x, transform.position.y);

                    if (vector.x > 0.0f)
                    {
                        rigidbody.velocity = new Vector2(-speed, 0.0f);
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(speed, 0.0f);
                    }

                    break;
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                foreach (ContactPoint2D point in collision.contacts)
                {
                    if (Vector2.Dot((point.point -
                        new Vector2(transform.position.x, transform.position.y)).normalized,
                        Vector2.up) > Mathf.Cos(Mathf.Deg2Rad * 45.0f))
                    {
                        GameObject.Destroy(gameObject);
                    }
                    else
                    {
                        GameObject.Destroy(collision.gameObject);
                    }

                    break;
                }
            }
        }
    }
}

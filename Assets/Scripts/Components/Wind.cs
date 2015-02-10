using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Components
{
    public class Wind : SystemComponent
    {
        public Vector2 force;
        public SpriteRenderer spriteRenderer;

        public void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
                playerRigidbody.AddForce(force);
            }
        }
    }
}

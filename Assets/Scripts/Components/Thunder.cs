using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Components
{
    public class Thunder : SystemComponent
    {
        public Rigidbody2D rigidbody;
        public float speed;
        public float livingTime;

        public void OnStart()
        {
            Destroy(gameObject, livingTime);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                GameObject.Destroy(other.gameObject);       
            }

            if (other.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}

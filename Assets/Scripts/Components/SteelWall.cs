using Content;
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Components
{
    public class SteelWall : SystemComponent
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();
                PhysicsBehaviour behaviour = player.behaviour;

                if (behaviour.canDash)
                {
                    player.dashObject = this;
                }
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            PhysicsBehaviour behaviour = player.behaviour;

            if (behaviour.canDash)
            {
                player.dashObject = null;
            }
        }
    }
}

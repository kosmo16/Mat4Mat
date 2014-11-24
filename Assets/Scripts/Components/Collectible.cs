using Content;
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Components
{
    public enum CollectibleType
    {
        Coin,
        Transform
    }

    public class Collectible : SystemComponent
    {
        public CollectibleType type;
        public int score;
        public PhysicsBehaviour behaviour;
        public bool collisionWithPlayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                collisionWithPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                collisionWithPlayer = false;
            }
        }
    }
}

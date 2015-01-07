using Content;
using Framework.Core;
using UnityEngine;

namespace Components
{
    public class Player : SystemComponent
    {
        public Rigidbody2D rigidbody;
        public PhysicsBehaviour behaviour;
        public ThunderSpawner thunderSpawner;
        public int score;
        public bool facingRight;
		public Animator animator;
    }
}

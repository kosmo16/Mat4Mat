using UnityEngine;

namespace Content
{
    public class PhysicsBehaviour : ScriptableObject
    {

        [Header("General")]
        public float moveForce;
        public float jumpForce;
        public float maxSpeed;
        public float mass;
        public PhysicMaterial material;

        [Header("Special")]
        public bool canDestroyObstacles;
        public bool canSpawnThunders;
        public bool canDash;
        public bool canSwim;
    }
}
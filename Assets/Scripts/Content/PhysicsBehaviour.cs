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

        [Header("Special")]
        public bool canDestroyObstacles;
    }
}
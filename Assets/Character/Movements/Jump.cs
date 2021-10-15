using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Jump : MonoBehaviour
    {
        [SerializeField, Min(0)]
        [Tooltip("Initial jump speed")]
        float jumpStrength = 10;

        Rigidbody rigidbody;
        GroundDetector ground;

        private void Start()
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
        }

        public void TryJump(CharacterInput input)
        {
            if (ground.OnGround)
                if (input.Jump)
                    rigidbody.velocity += Vector3.up * jumpStrength;
        }
    }
}

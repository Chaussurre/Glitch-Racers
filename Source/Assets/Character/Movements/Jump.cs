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
        [SerializeField, Range(0, 1)]
        [Tooltip("Allows the character to jump higher when they hold jump")]
        float HoldJumpGravitySensibility = 1;


        Rigidbody rb;
        GroundDetector ground;
        GravityApply gravity;

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
            gravity = GetComponentInChildren<GravityApply>();
        }

        public void TryJump(CharacterInput input)
        {
            if (ground.Ground == GroundDetector.GroundType.Walkable)
                if (input.Jump)
                {
                    rb.velocity += transform.up * jumpStrength;
                    gravity.SetSensibility("jump", HoldJumpGravitySensibility);
                }

            if (!input.HoldJump || Vector3.Dot(rb.velocity, transform.up) < 0)
                gravity.RemoveSensibility("jump");
        }
    }
}

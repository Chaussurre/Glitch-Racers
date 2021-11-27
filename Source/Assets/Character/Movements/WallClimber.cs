using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class WallClimber : MonoBehaviour
    {
        [SerializeField, Range(0, 1)]
        [Tooltip("The gravity sensibilty for the duration of the wall climb. Lower means longer wall climb.")]
        float WallClimbGravitySensibility = 0.5f;

        Rigidbody rb;
        GroundDetector ground;
        GravityApply gravity;
        GrapplingHookShooter grapplingHook;

        GameObject wall = null;
        Vector3 normal;

        readonly string gravitySensitivityName = "Wall Climb";

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
            gravity = GetComponentInChildren<GravityApply>();
            grapplingHook = GetComponentInChildren<GrapplingHookShooter>();
        }

        public void TryWallClimb()
        {
            if (ground.Ground == GroundDetector.GroundType.WallClimbable && !grapplingHook.IsHooking)
                StartWallClimb();
            else if (ground.Ground == GroundDetector.GroundType.Walkable) //Reached ground
                StopWallClimb();
            else if (gravity.Falling) //Stoped climbing
                StopWallClimb();
            else if (!Physics.Raycast(transform.position, -normal, out RaycastHit hit) || hit.collider.gameObject != wall) //No longer next to the wall
                StopWallClimb();
            else if (grapplingHook.IsHooking) //started hooking
                StopWallClimb();

            if(wall) WallClimb();
        }

        void StartWallClimb()
        {
            gravity.SetSensibility(gravitySensitivityName, WallClimbGravitySensibility);
            wall = ground.CollidingObject;
            normal = ground.Normal.normalized;
        }

        void StopWallClimb()
        {
            gravity.RemoveSensibility(gravitySensitivityName);
            wall = null;
        }

        void WallClimb()
        {
            rb.velocity = Vector3.Project(rb.velocity, Vector3.up);
            rb.velocity += -normal;
        }
    }
}

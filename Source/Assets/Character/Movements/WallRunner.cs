using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class WallRunner : MonoBehaviour
    {
        [SerializeField, Range(0, 1)]
        [Tooltip("The gravity sensibilty for the duration of the wall run. Lower means longer wall runs.")]
        float WallRunGravitySensibility;

        [SerializeField, Min(0)]
        [Tooltip("The minimum speed the character wall run")]
        float WallRunSpeed;

        new Rigidbody rigidbody;
        GroundDetector ground;
        GravityApply gravity;
        GrapplingHookShooter grapplingHook;
        CharacterRotation characterRotation;
        
        bool wallRunning = false;
        GameObject wall = null;
        Vector3 normal;
        Vector3 direction;

        readonly string ActionLockName = "Wall Run";

        private void Start()
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
            gravity = GetComponentInChildren<GravityApply>();
            grapplingHook = GetComponentInChildren<GrapplingHookShooter>();
            characterRotation = GetComponentInChildren<CharacterRotation>();
        }

        public void TryWallRun()
        {
            if (ground.Ground == GroundDetector.GroundType.WallRunnable && !grapplingHook.IsHooking)
                StartWallRun();
            else if (ground.Ground == GroundDetector.GroundType.Walkable) //Reached ground
                StopWallRun();
            else if (!Physics.Raycast(transform.position, -normal, out RaycastHit hit) || hit.collider.gameObject != wall) //No longer next to the wall
                StopWallRun();
            else if (grapplingHook.IsHooking) //Started to hook something
                StopWallRun();

            if(wallRunning)
            {
                WallRun();
                rigidbody.velocity += -normal;
            }
        }

        void StartWallRun()
        {
            if (!wallRunning)
            {
                wallRunning = true;
                characterRotation.SetIsLocked(ActionLockName);
                gravity.SetSensibility(ActionLockName, WallRunGravitySensibility);
                wall = ground.CollidingObject;
                normal = ground.Normal.normalized;
                direction = Vector3.Cross(transform.up, normal);
                if (Vector3.Dot(transform.forward, direction) < 0)
                    direction *= -1;
            }
        }

        void StopWallRun()
        {
            wallRunning = false;
            gravity.RemoveSensibility(ActionLockName);
            characterRotation.RemoveIsLocked(ActionLockName);
            wall = null;
        }
        
        public void WallRun()
        {
            Vector3 fall = Vector3.Project(rigidbody.velocity, transform.up);
            Vector3 flat = Vector3.Project(rigidbody.velocity, direction);

            if (flat.magnitude > WallRunSpeed)
                return;

            rigidbody.velocity = direction.normalized * WallRunSpeed + fall;
        }
    }
}

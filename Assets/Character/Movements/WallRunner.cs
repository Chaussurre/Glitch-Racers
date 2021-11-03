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
        
        bool wallRunning = false;
        GameObject wall = null;
        Vector3 normal;
        Vector3 direction;

        readonly string gravitySensitivityName = "Wall Run";

        private void Start()
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
            gravity = GetComponentInChildren<GravityApply>();
        }

        public void TryWallRun()
        {
            if (ground.Ground == GroundDetector.GroundType.WallRunnable)
                StartWallRun();
            else if (ground.Ground == GroundDetector.GroundType.Walkable) //Reached ground
                StopWallRun();
            else if (!Physics.Raycast(transform.position, -normal, out RaycastHit hit) || hit.collider.gameObject != wall) //No longer next to the wall
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
                gravity.SetSensibility(gravitySensitivityName, WallRunGravitySensibility);
                wall = ground.CollidingObject;
                normal = ground.Normal.normalized;
                direction = Vector3.Cross(Vector3.up, normal);
                if (Vector3.Dot(transform.forward, direction) < 0)
                    direction *= -1;
            }
        }

        void StopWallRun()
        {
            wallRunning = false;
            gravity.RemoveSensibility(gravitySensitivityName);
            wall = null;
        }
        
        public void WallRun()
        {
            Vector3 fall = Vector3.Project(rigidbody.velocity, Vector3.up);
            Vector3 flat = Vector3.Project(rigidbody.velocity, direction);

            if (flat.magnitude > WallRunSpeed)
                return;

            rigidbody.velocity = direction.normalized * WallRunSpeed + fall;
        }
    }
}

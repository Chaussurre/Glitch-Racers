using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHook : MonoBehaviour
    {
        [SerializeField, Range(0, 10)]
        [Tooltip("The acceleration the character can have while moving and balancing")]
        float HookedAcceleration;


        GameObject hookedObject;
        public Vector3 HookedPoint { get { return hookedObject.transform.position + relativPos; } }
        public bool Hooking { get { return hookedObject != null; } }

        Vector3 relativPos;
        Rigidbody rb;

        public float MaxDistance { get; private set; }

        public bool IsAtMaxDistance { get { return Vector3.Distance(rb.transform.position, HookedPoint) >= MaxDistance - 0.1f; } }

        public void Hook(RaycastHit hit)
        {
            hookedObject = hit.collider.gameObject;
            relativPos = hit.point - hookedObject.transform.position;
            transform.parent = null;
            MaxDistance = (hit.point - rb.transform.position).magnitude;
        }

        public void ResetHook()
        {
            hookedObject = null;
        }

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        public void UpdateHook(CharacterInput input)
        {
            if (Hooking)
                WhileHooking(input);
        }

        void WhileHooking(CharacterInput input)
        {
            Vector3 relativBodyPos = rb.transform.position - HookedPoint;

            if (relativBodyPos.magnitude >= MaxDistance)
            {
                rb.velocity += Vector3.ProjectOnPlane(input.WalkDirection, Vector3.up) * HookedAcceleration * Time.deltaTime;

                rb.transform.position = HookedPoint + relativBodyPos.normalized * MaxDistance;
                if (Vector3.Dot(rb.velocity, relativBodyPos) > 0)
                    rb.velocity = Vector3.ProjectOnPlane(rb.velocity, relativBodyPos);
            }
        }
    }
}

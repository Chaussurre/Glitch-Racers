using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHook : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The acceleration the character can have while moving and balancind")]
        Transform hand;
        [SerializeField, Range(0, 10)]
        [Tooltip("The acceleration the character can have while moving and balancind")]
        float HookedAcceleration;

        GameObject hookedObject;
        Vector3 hookedPoint;
        public bool Hooking { get { return hookedObject != null; } }

        Vector3 relativPos;
        Rigidbody rb;

        float MaxDistance;

        public void Hook(RaycastHit hit)
        {
            hookedPoint = hit.point;
            hookedObject = hit.collider.gameObject;
            relativPos = hit.point - hookedObject.transform.position;
            transform.parent = null;
            MaxDistance = (hit.point - rb.transform.position).magnitude;
        }

        public void ResetHook()
        {
            hookedObject = null;
            transform.parent = rb.transform;
            transform.localScale = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        public void UpdqteHook(CharacterInput input)
        {
            if (Hooking)
                WhileHooking(input);
        }

        void WhileHooking(CharacterInput input)
        {
            Vector3 relativBodyPos = rb.transform.position - hookedPoint;
            Vector3 relativHandPos = hand.position - hookedPoint;

            transform.position = Vector3.Lerp(hookedObject.transform.position + relativPos, hand.position, 0.5f);
            transform.localScale = new Vector3(.1f, relativHandPos.magnitude / 2, .1f);
            transform.rotation = Quaternion.LookRotation(relativHandPos);
            transform.rotation = Quaternion.LookRotation(transform.up);

            if (relativBodyPos.magnitude >= MaxDistance)
            {
                rb.velocity += Vector3.ProjectOnPlane(input.WalkDirection, Vector3.up) * HookedAcceleration * Time.deltaTime;

                rb.transform.position = hookedPoint + relativBodyPos.normalized * MaxDistance;
                if (Vector3.Dot(rb.velocity, relativBodyPos) > 0)
                    rb.velocity = Vector3.ProjectOnPlane(rb.velocity, relativBodyPos);
            }
        }
    }
}

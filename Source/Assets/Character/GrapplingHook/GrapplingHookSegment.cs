using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHookSegment : MonoBehaviour
    {
        [SerializeField]
        public Transform StartPoint;
        [SerializeField]
        public Transform EndPoint;

        [SerializeField, Min(0)]
        float MaxSpeed = 10;

        Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public Vector3 SetAnchorToPoint(Vector3 position, bool AnchorIsStart = true)
        {
            Transform anchor = AnchorIsStart? StartPoint : EndPoint;
            Transform rotationCenter = AnchorIsStart ? EndPoint : StartPoint;

            Vector3 current = (anchor.position - rotationCenter.position).normalized;
            Vector3 target = (position - rotationCenter.position).normalized;
            float angleUp = Vector3.SignedAngle(current, target, Vector3.up);
            transform.RotateAround(rotationCenter.position, Vector3.up, angleUp);

            current = (anchor.position - rotationCenter.position).normalized;
            float angleRight = Vector3.SignedAngle(current, target, Vector3.right);
            transform.RotateAround(rotationCenter.position, Vector3.right, angleRight);

            current = (anchor.position - rotationCenter.position).normalized;
            float angleForward = Vector3.SignedAngle(current, target, Vector3.forward);
            transform.RotateAround(rotationCenter.position, Vector3.forward, angleForward);

            if (rb.velocity.magnitude > MaxSpeed)
                rb.velocity = rb.velocity.normalized * MaxSpeed;
            transform.position += position - anchor.position;
            return rotationCenter.position;
        }
    }
}

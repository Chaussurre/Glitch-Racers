using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    public class LedgeCatcher : MonoBehaviour
    {
        [Tooltip("The height the ledge must be at min to be grabed")]
        [SerializeField, Range(0, 4)]
        float LedgeMinHeight;

        [Tooltip("The height the ledge must be at max to be grabed")]
        [SerializeField, Range(0, 4)]
        float LedgeMaxHeight;

        Rigidbody rb;
        GroundDetector ground;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            ground = GetComponent<GroundDetector>();
        }

        public void TryCatchLedge()
        {
            if (Catching())
                OnLedge();
        }

        void OnLedge()
        {
            rb.velocity = Vector3.zero;

        }

        bool Catching()
        {
            if (ground.Ground != GroundDetector.GroundType.WallClimbable)
                return false;

            if (Vector3.Dot(rb.velocity, Vector3.down) < 0)
                return false;

            if (Physics.Raycast(transform.position + Vector3.up * LedgeMaxHeight, transform.forward, out RaycastHit hit, 1f) &&
                hit.collider.gameObject == ground.CollidingObject)
                return false;

            float? height = GetLedgeHeight();
            if (!height.HasValue || height.Value < LedgeMinHeight)
                return false;

            return true;
        }

        float? GetLedgeHeight()
        {
            Vector3 origin = transform.position + Vector3.up * LedgeMaxHeight + transform.forward;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit))
            {
                Debug.DrawLine(origin, hit.point);
                return LedgeMaxHeight - Vector3.Distance(origin, hit.point);
            }

            return null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + Vector3.up * LedgeMaxHeight, transform.forward);
            Handles.Label(transform.position + Vector3.up * LedgeMaxHeight, "Ledge Max Height");
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + Vector3.up * LedgeMinHeight, transform.forward);
            Handles.Label(transform.position + Vector3.up * LedgeMinHeight, "Ledge Min Height");
        }
#endif
    }
}
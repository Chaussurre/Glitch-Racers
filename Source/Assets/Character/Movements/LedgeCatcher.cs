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
        GrapplingHookShooter grapplingHook;
        CharacterRotation characterRotation;

        Vector3? CatchedPoint = null;
        bool climbing = false;

        readonly string ActionLockName = "Ledge catcher";

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            ground = GetComponent<GroundDetector>();
            grapplingHook = GetComponent<GrapplingHookShooter>();
            characterRotation = GetComponent<CharacterRotation>();
        }

        public void TryCatchLedge(CharacterInput input)
        {
            if (!CatchedPoint.HasValue)
                Catching();

            if(CatchedPoint.HasValue)
                OnLedge(input) ;
        }

        void OnLedge(CharacterInput input)
        {
            rb.velocity = Vector3.zero;

            if (grapplingHook.IsHooking)
            {
                characterRotation.RemoveIsLocked(ActionLockName);
                CatchedPoint = null;
                return;
            }

            if(input.Jump && !climbing)
                StartCoroutine(Climb());
        }

        IEnumerator Climb()
        {
            climbing = true;
            characterRotation.SetIsLocked(ActionLockName);
            Vector3 start = transform.position;
            Vector3 mid = transform.position + Vector3.up * (LedgeMaxHeight + .3f) 
                + Vector3.Project(CatchedPoint.Value - transform.position, Vector3.up);
            Vector3 end = CatchedPoint.Value + Vector3.up * (LedgeMaxHeight + .3f);
            float stepTime = 0.5f;
            

            for (float time = 0; time < stepTime; time += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(start, mid, time / stepTime);
                yield return new WaitForEndOfFrame();
            }
            transform.position = mid;

            for (float time = 0; time < stepTime; time += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(mid, end, time / stepTime);
                yield return new WaitForEndOfFrame();
            }

            characterRotation.RemoveIsLocked(ActionLockName);
            transform.position = end;
            climbing = false;
            CatchedPoint = null;
        }

        void Catching()
        {
            CatchedPoint = null;

            if (grapplingHook.IsHooking)
                return;

            if (ground.Ground == GroundDetector.GroundType.Walkable)
                return;

            if (Vector3.Dot(rb.velocity, Vector3.down) < 0)
                return;

            if (Physics.Raycast(transform.position + Vector3.up * LedgeMaxHeight, transform.forward, out RaycastHit hit, 1f) &&
                hit.collider.gameObject == ground.CollidingObject)
                return;

            float? height = GetLedgeHeight(out Vector3? point);
            if (height.HasValue && height.Value > LedgeMinHeight)
            {
                characterRotation.SetIsLocked(ActionLockName);
                CatchedPoint = point;
            }
        }

        float? GetLedgeHeight(out Vector3? point)
        {
            Vector3 origin = transform.position + Vector3.up * LedgeMaxHeight + transform.forward;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit))
            {
                point = hit.point;
                return LedgeMaxHeight - Vector3.Distance(origin, hit.point);
            }

            point = null;
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
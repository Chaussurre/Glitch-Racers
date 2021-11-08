using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    public class PathTarget : MonoBehaviour
    {
        [SerializeField]
        private bool jump;
        //private Vector3 startJump;
        //private Vector3 endJump;

        [SerializeField]
        private Vector3 jumpDirection;
        
        
        
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1);
        }
        
        void OnDrawGizmosSelected()
        {
            if (jump)
            {
                Handles.Label(transform.position + jumpDirection, "jump");
                Gizmos.color = Color.magenta;
                for (float d = 0.1f; d < jumpDirection.magnitude; d += 0.1f)
                {
                    Gizmos.DrawLine(transform.position + GetCurvePoint(d-0.1f), transform.position + GetCurvePoint(d));
                }
            }
        }

        Vector3 GetCurvePoint(float distance)
        {
            return jumpDirection.normalized * distance;
        }
#endif
    }
}

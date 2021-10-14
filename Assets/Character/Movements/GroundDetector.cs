using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    /** Detects when the character is on walkable ground
     */
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField, Range(0, 90)]
        [Tooltip("The angle at wich the ground is still walkable")]
        float AngleTolerance = 10;


        readonly private HashSet<Collision> collisions = new HashSet<Collision>();

        private Vector3 normal;
        //! true if the character is on walkable ground
        public bool OnGround
        {
            get
            {
                return Vector3.Angle(Vector3.up, normal) <= AngleTolerance;
            }
        }

        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            normal = Vector3.zero;
            foreach (var col in collisions)
                for (int i = 0; i < col.contactCount; i++)
                {
                    Vector3 v = col.GetContact(i).normal;
                    if (v.y > normal.y)
                        normal = v;
                }
        }

        private void OnCollisionEnter(Collision collision)
        {
            collisions.Add(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            collisions.Remove(collision);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            int rayCount = 20;
            for (int i = 0; i < rayCount; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawRay(transform.position + Vector3.down, Quaternion.Euler(0, 360 / rayCount * i, AngleTolerance) * Vector3.up);
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + Vector3.down, Quaternion.Euler(0, 360 / rayCount * i, AngleTolerance + 90) * Vector3.up);
            }
        }
#endif
    }
}

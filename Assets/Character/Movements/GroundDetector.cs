using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** Detects when the character is on walkable ground
     */
    public class GroundDetector : MonoBehaviour
    {

        readonly HashSet<Collision> collisions = new HashSet<Collision>();

        Vector3 normal;
        //! true if the character is on walkable ground
        public bool OnGround
        {
            get
            {
                return normal == Vector3.up;
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
    }
}

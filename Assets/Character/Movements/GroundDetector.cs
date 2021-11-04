using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    //! Detects what type of ground the character is on
    public class GroundDetector : MonoBehaviour
    {
        public enum GroundType
        {
            None, Walkable, WallRunnable, WallClimbable
        }

        [SerializeField, Range(0, 90)]
        [Tooltip("The angle at wich the ground is still walkable")]
        float WalkAngleTolerance = 30;
        [SerializeField, Range(0, 90)]
        [Tooltip("The angle at which a wall can be wall runned")]
        float WallRunAngleVerticalTolerance = 5;
        [SerializeField, Range(0, 90)]
        [Tooltip("The angle at which the character must face the wall to wall climb")]
        float WallClimbAngleHorizontalTolerance = 30;


        readonly private Dictionary<Collider, Vector3> normals = new Dictionary<Collider, Vector3>();

        private Vector3 normal;
        private GameObject collindingObject;
        public Vector3 Normal { get { return normal; } } //Split normal in two fields for debug reasons
        public GameObject CollidingObject { get { return collindingObject; } }
        
        //! The type of ground the character is interacting with
        public GroundType Ground;


        private void Update()
        {
            normal = Vector3.down;
            collindingObject = null;
            foreach (var pair in normals)
                if (pair.Value.y > normal.y)
                {
                    normal = pair.Value;
                    collindingObject = pair.Key.gameObject;
                }

            Ground = UpdateGourndType();
        }
        
        GroundType UpdateGourndType()
        {
            if (normal.sqrMagnitude == 0)
                return GroundType.None;

            float angleVertical = Vector3.Angle(Vector3.up, normal);
            if (angleVertical <= WalkAngleTolerance)
                return GroundType.Walkable;

            float angleHorizontal = Vector3.SignedAngle(transform.forward, -normal, Vector3.up);

            if (Mathf.Abs(angleVertical - 90) < WallRunAngleVerticalTolerance)
                if (Mathf.Abs(angleHorizontal) < WallClimbAngleHorizontalTolerance)
                    return GroundType.WallClimbable;
                else
                    return GroundType.WallRunnable;

            return GroundType.None;
        }

        private void OnCollisionStay(Collision collision)
        {
            Vector3? n = null;
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (!n.HasValue || (normal.y > n.Value.y && normal.sqrMagnitude > 0.01f))
                    n = collision.GetContact(i).normal;
            }

            normals[collision.collider] = n.Value;
        }


        private void OnCollisionExit(Collision collision)
        {
            normals.Remove(collision.collider);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            int rayCount = 20;
            for (int i = 0; i < rayCount; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawRay(transform.position + Vector3.down * 2, Quaternion.Euler(0, 360 / rayCount * i, WalkAngleTolerance) * Vector3.up);
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + Vector3.down * 2, Quaternion.Euler(0, 360 / rayCount * i, WalkAngleTolerance + 90) * Vector3.up);

                if(Normal != Vector3.zero)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(transform.position, Normal * 10);
                }
            }
        }
#endif
    }
}

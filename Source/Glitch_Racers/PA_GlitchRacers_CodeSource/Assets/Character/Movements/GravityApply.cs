using Map.GravityModify;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \brief Control the gravity applied to the charcter
     */
    public class GravityApply : MonoBehaviour
    {
        [Tooltip("The speed the character change rotation when changing gravity zone")]
        [SerializeField, Range(0, 360)]
        float RotationSpeed;

        Rigidbody rb;

        public Vector3 testGravityDirection;

        Vector3 GravityDirection
        {
            get
            {
                return modifiers.Count == 0 ? Vector3.down 
                    : modifiers[modifiers.Count - 1].GetGravityDirection(transform.position);
            }
        }


        readonly List<GravityModifier> modifiers = new List<GravityModifier>();

        readonly private Dictionary<string, float> sensibilities = new Dictionary<string, float>();

        //! Change the character's sensibility to gravity by given ratio. Sensibilities are stored by name
        public void SetSensibility(string name, float val)
        {
            val = Mathf.Max(val, 0);
            if (sensibilities.ContainsKey(name))
                sensibilities[name] = val;
            else
                sensibilities.Add(name, val);
        }

        //! Reverse the change done by a specific sensibility
        public void RemoveSensibility(string name)
        {
            if (sensibilities.ContainsKey(name))
                sensibilities.Remove(name);
        }

        //! How much is this object affected by gravity
        public float GravitySensibilty
        {
            get
            {
                float res = 1;
                foreach (var pair in sensibilities)
                    res *= pair.Value;
                return res;
            }
        }

        public bool IsFalling { get { return Vector3.Dot(rb.velocity, GravityDirection) > 0; } }

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
        }

        public void GravityPush()
        {
            if (!isActiveAndEnabled)
                return;

            float angle = Vector3.SignedAngle(transform.up, -GravityDirection, transform.right);
            transform.Rotate(Vector3.right, Mathf.Clamp(angle, -RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime));
            angle = Vector3.SignedAngle(transform.up, -GravityDirection, transform.forward);
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime));

            rb.velocity += GravityDirection * Physics.gravity.magnitude * GravitySensibilty * Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out GravityModifier modifier))
            {
                if (other.ClosestPoint(transform.position) == transform.position)
                {
                    if (!modifiers.Contains(modifier))
                        modifiers.Add(modifier);
                }
                else
                    modifiers.Remove(modifier);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GravityModifier modifier))
                modifiers.Remove(modifier);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \brief Control the gravity applied to the charcter
     */
    public class GravityApply : MonoBehaviour
    {
        Rigidbody rb;


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

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
        }


        public void GravityPush()
        {
            if (!isActiveAndEnabled)
                return;

            rb.AddForce(Physics.gravity * GravitySensibilty);
        }
    }
}

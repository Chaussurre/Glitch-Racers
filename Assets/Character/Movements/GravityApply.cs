using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \brief Control the gravity applied to the charcter
     */
    public class GravityApply : MonoBehaviour
    {
        Rigidbody rigidbody;

        //! How much is this object affected by gravity
        [HideInInspector]
        public float GravitySensibilty = 1;

        private void Start()
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
        }


        public void GravityPush()
        {
            if (!isActiveAndEnabled)
                return;

            rigidbody.AddForce(Physics.gravity * GravitySensibilty);
        }
    }
}

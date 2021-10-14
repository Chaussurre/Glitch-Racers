using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
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
            rigidbody.AddForce(Physics.gravity * GravitySensibilty);
        }
    }
}

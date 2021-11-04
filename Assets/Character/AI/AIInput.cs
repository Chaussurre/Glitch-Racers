using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.IA
{
    //! Handle the decisions for the AI
    public class AIInput : InputController
    {
        [SerializeField]
        GameObject target;

        [SerializeField]
        GameObject body;

        protected override Vector3 GetWalkDirection()
        {
            return target.transform.position - body.transform.position;
        }

        protected override Vector3 GetCameraDirection()
        {
            float horizontal = Vector3.SignedAngle(body.transform.forward, target.transform.position - body.transform.position, Vector3.up);
            float vertical = Vector3.SignedAngle(body.transform.forward, target.transform.position - body.transform.position, body.transform.right);

            return new Vector3(horizontal, vertical);
        }
    }
}

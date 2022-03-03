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
        private Path path;
        
        Vector3 target;

        [SerializeField]
        GameObject body;

        protected override Vector3 GetWalkDirection()
        {
            return target - body.transform.position;
        }

        protected override Vector3 GetCameraDirection()
        {
            float horizontal = Vector3.SignedAngle(body.transform.forward, target - body.transform.position, body.  transform.up);
            float vertical = Vector3.SignedAngle(body.transform.forward, target - body.transform.position, body.transform.right);
            Debug.Log(horizontal);
            return new Vector3(horizontal, vertical);
        }

        private void Update()
        {
            target = path.getNextTarget(body);
        }
        
    }
}

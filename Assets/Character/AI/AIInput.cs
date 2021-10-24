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

        //! \todo implement GetDirection
        protected override Vector3 GetWalkDirection()
        {
            return target.transform.position - body.transform.position;
        }
    }
}

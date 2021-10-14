using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.IA
{
    //! Handle the decisions for the AI
    public class AIInput : InputController
    {
        //! \todo implement GetDirection
        protected override Vector3 GetWalkDirection()
        {
            throw new NotImplementedException();
        }
    }
}

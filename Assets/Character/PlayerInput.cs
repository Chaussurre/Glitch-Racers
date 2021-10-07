using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerInput : InputController
    {
        protected override Vector3 GetDirection()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            return new Vector3(x, 0, z);
        }
    }
}
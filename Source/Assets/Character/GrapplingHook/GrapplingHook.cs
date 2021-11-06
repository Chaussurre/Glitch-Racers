using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHook : MonoBehaviour
    {
        public void Hook(Vector3 position)
        {
            transform.position = position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    //! Manage the Camera
    public class CameraRotator : MonoBehaviour
    {
        //! Speed of the rotation
        public float speed;

        //! Rotation of the camera
        void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

}


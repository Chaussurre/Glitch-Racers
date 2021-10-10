using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    //! Manage the Camera
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Speed of the rotation")]
        float speed;

        //! Rotation of the camera
        void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

}


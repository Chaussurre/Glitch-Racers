using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    //! InputController for the player
    public class PlayerInput : InputController
    {
        Rigidbody rb;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            rb = GetComponentInChildren<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override Vector3 GetWalkDirection()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            return x * rb.transform.right + z * rb.transform.forward;
        }

        protected override bool GetJump(out bool hold)
        {
            hold = Input.GetButton("Jump");
            return Input.GetButtonDown("Jump");
        }

        protected override Vector3 GetCameraDirection()
        {
            Vector3 camDirection = new Vector3();
            camDirection.x = Input.GetAxis("Mouse X");
            camDirection.y = Input.GetAxis("Mouse Y");

            return camDirection;
        }

        protected override bool GetHooking()
        {
            return Input.GetMouseButton(0);
        }
    }
}
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
            rb = GetComponentInChildren<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override Vector3 GetWalkDirection()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Quaternion rotationInput = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, rb.transform.forward, Vector3.up), 0);
            
            return rotationInput * new Vector3(x, 0, z);
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
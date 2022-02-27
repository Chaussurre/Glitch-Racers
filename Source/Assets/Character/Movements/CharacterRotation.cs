using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \todo Documentation
     */
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField, Range(-70, -15)]
        [Tooltip("comment")]
        private int minAngle = -30;
        
        [Range(30, 80)]
        public int maxAngle = 45;
        
        [Range(50, 500)]
        public int sensitivity = 200;
        
        [SerializeField]
        [Tooltip("The target object for vertical rotations")]
        private Transform targetTransform;
        
        //
        private Vector3 initVector;
        
        private bool IsLocked
        {
            get => false;
            set => IsLocked = LockCharacter();
        }
        
        //Raycast to check if the camera hits a wall
        public void CheckCollision()
        {
            Vector3 direction = cameraTransform.position - targetTransform.position;
            if (Physics.Raycast(targetTransform.position, direction, out RaycastHit hit, initVector.magnitude))
            { 
                cameraTransform.position = hit.point;
                Debug.Log("Did Hit");
            }
            else
            {
                cameraTransform.localPosition = initVector;
                Debug.Log("Did not Hit");
            }
        }
        
        private readonly HashSet<string> lockedList = new HashSet<string>();
        
        private Vector3 rotation;
        private Vector3 target;

        private void Start()
        {
            rotation = transform.rotation.eulerAngles;
            target = targetTransform.transform.localRotation.eulerAngles;
            initVector = cameraTransform.localPosition;
        }

        //! Store the status of the name passed in parameter in a hashset
        public void SetIsLocked(string funcName)
        {
            lockedList.Add(funcName);
        }

        public void RemoveIsLocked(string funcName)
        {
            lockedList.Remove(funcName);
        }

        public void LookAround(CharacterInput input)
        {
            Vector3 target = targetTransform.localEulerAngles;

            if (target.x > 180)
                target.x -= 360;
            target.x -= input.Camera.y * sensitivity * Time.deltaTime;
            target.x = Mathf.Clamp(target.x, minAngle, maxAngle);

            if(!IsLocked)
            {
                transform.Rotate(Vector3.up, input.Camera.x * sensitivity * Time.deltaTime + target.y);
                target.y = 0;
            }
            else
            {
                target.y += input.Camera.x * sensitivity * Time.deltaTime;
            }

            targetTransform.localEulerAngles = target;
        }
    }
}
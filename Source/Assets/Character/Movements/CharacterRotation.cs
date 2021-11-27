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

        private bool IsLocked
        {
            get => true;
            set => IsLocked = LockCharacter();
        }
        
        private readonly HashSet<string> lockedList = new();

        private Vector3 rotation;
        private Vector3 target;

        private void Start()
        {
            rotation = transform.rotation.eulerAngles;
            target = targetTransform.transform.localRotation.eulerAngles;
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
        
        //! Lock the camera if any of the values stored in the dictionary is true
        public bool LockCharacter()
        {
            return lockedList.Count != 0;
        }

        public void LookAround(CharacterInput input)
        {
            target.x -= input.Camera.y * sensitivity * Time.deltaTime;
            target.x = Mathf.Clamp(target.x, minAngle, maxAngle);

            if(!IsLocked)
            {
                rotation.y += target.y;
                target.y = 0;
                rotation.y += input.Camera.x * sensitivity * Time.deltaTime;
                transform.localEulerAngles = rotation;
            }
            else
            {
                target.y += input.Camera.x * sensitivity * Time.deltaTime;
            }
            
            targetTransform.localEulerAngles = target;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \todo Documentation
     */
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField, Range(-45, -15)]
        [Tooltip("comment")]
        private int minAngle = -30;
        [Range(30, 80)]
        public int maxAngle = 45;
        [Range(50, 500)]
        public int sensitivity = 200;
        [SerializeField]
        [Tooltip("The target object for vertical rotations")]
        Transform TargetTransform;

        [SerializeField]
        private bool isLocked;
        
        Vector3 rotation;
        Vector3 target;

        private void Start()
        {
            rotation = transform.rotation.eulerAngles;
            target = TargetTransform.transform.localRotation.eulerAngles;
        }

        public void LockCharacter(bool locked)
        {
            isLocked = locked;
        }

        public void LookAround(CharacterInput input)
        {
            target.x -= input.Camera.y * sensitivity * Time.deltaTime;
            target.x = Mathf.Clamp(target.x, minAngle, maxAngle);

            if(!isLocked)
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
            
            TargetTransform.localEulerAngles = target;
        }
    }
}
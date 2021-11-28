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

        public bool IsLocked
        {
            get { return lockedList.Count > 0; }
        }
        
        private readonly HashSet<string> lockedList = new HashSet<string>();

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
                transform.Rotate(Vector3.up, input.Camera.x * sensitivity * Time.deltaTime);
            }
            else
            {
                //target = Quaternion.AngleAxis(input.Camera.x * sensitivity * Time.deltaTime, transform.up) * tar;
            }

            targetTransform.localEulerAngles = target;
        }
    }
}
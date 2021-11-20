using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("TargetTransform")]
        [SerializeField]
        [Tooltip("The target object for vertical rotations")]
        private Transform targetTransform;

        private bool _isLocked;

        private readonly Dictionary<string, bool> _lockedList = new();

        private Vector3 _rotation;
        private Vector3 _target;

        private void Start()
        {
            _rotation = transform.rotation.eulerAngles;
            _target = targetTransform.transform.localRotation.eulerAngles;
        }
        
        //! Store the status of the name passed in parameter in a dictionary
        public void SetIsLocked(string funcName, bool lockStatus)
        {
            _lockedList.Add(funcName, lockStatus);
        }
        
        //! Lock the camera if any of the values stored in the dictionary is true
        public void LockCharacter()
        {
            foreach (var locked in _lockedList)
            {
                if (locked.Value)
                {
                    _isLocked = true;
                }
            }
        }

        public void LookAround(CharacterInput input)
        {
            _target.x -= input.Camera.y * sensitivity * Time.deltaTime;
            _target.x = Mathf.Clamp(_target.x, minAngle, maxAngle);

            if(!_isLocked)
            {
                _rotation.y += _target.y;
                _target.y = 0;
                _rotation.y += input.Camera.x * sensitivity * Time.deltaTime;
                transform.localEulerAngles = _rotation;
            }
            else
            {
                _target.y += input.Camera.x * sensitivity * Time.deltaTime;
            }
            
            targetTransform.localEulerAngles = _target;
        }
    }
}
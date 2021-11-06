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

        Vector3 rotation;
        Vector3 target;

        private void Start()
        {
            rotation = transform.rotation.eulerAngles;
            target = TargetTransform.transform.localRotation.eulerAngles;
        }

        public void LookAround(CharacterInput input)
        {
            target.x -= input.Camera.y * sensitivity * Time.deltaTime;
            rotation.y += input.Camera.x * sensitivity * Time.deltaTime;
            target.x = Mathf.Clamp(target.x, minAngle, maxAngle);

            TargetTransform.localEulerAngles = target;
            transform.localEulerAngles = rotation;
        }
    }
}
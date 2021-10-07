using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{ 
    //! Control the movement of any given character
    public class Movement : MonoBehaviour
    {
        Rigidbody Rigidbody;

        [Header("Stats:")]
        [SerializeField]
        float speedForward = 1;
        
        //! \todo Let side walking be independant from forward walking
        //float speedSide;

        protected virtual void Awake()
        {
            Rigidbody = GetComponentInChildren<Rigidbody>();
        }

        private void Update()
        {
            CharacterInput input = GetComponent<InputController>().GetInput();
            Move(input);
        }

        void Move(CharacterInput input)
        {
            Vector3 speedVec = input.Direction * speedForward;

            Rigidbody.velocity = new Vector3(speedVec.x, Rigidbody.velocity.y, speedVec.z);
        }
    }
}
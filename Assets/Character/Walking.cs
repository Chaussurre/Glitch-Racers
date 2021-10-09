using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{ 
    //! Control the movement of any given character
    public class Walking : MonoBehaviour
    {
        Rigidbody Rigidbody;

        [Header("Stats:")]
        [SerializeField, Min(0)]
        [Tooltip("When the character goes from not moving to moving, this is the speed they starts at")]
        float minSpeedForward = 1;
        [SerializeField, Min(0)]
        [Tooltip("when walking in a line, the character will accelerate until they reach their max speed")]
        float acceleration = 1;
        [SerializeField, Min(0)]
        [Tooltip("When the character run at that speed, they can't accelerate anymore")]
        float maxSpeedForward = 2;
        [SerializeField, Min(0)]
        [Tooltip("The speed the character walks from side to side")]
        float speedSide = 1;

        protected virtual void Awake()
        {
            Rigidbody = GetComponentInChildren<Rigidbody>();
        }

        //! Walk the character in the given direction
        public void Move(CharacterInput input)
        {
            input.Direction.Normalize();
            Vector3 forwardVec = Vector3.Project(input.Direction, transform.forward);
            Vector3 sideVec = input.Direction - forwardVec;


            ApplyMove(forwardVec, minSpeedForward, acceleration);
            ApplyMove(sideVec, speedSide, 0);
        }

        void ApplyMove(Vector3 Direction, float Min, float acceleration)
        {
            float currentSpeed = Vector3.ProjectOnPlane(Rigidbody.velocity, Vector3.up).magnitude;

            if (currentSpeed < Min)
            {
                Rigidbody.velocity = Direction * Min + Rigidbody.velocity;
                currentSpeed = Vector3.ProjectOnPlane(Rigidbody.velocity, Vector3.up).magnitude;
                if (currentSpeed > Min)
                    Rigidbody.velocity = Rigidbody.velocity.normalized * Min;
            }
            else
                Rigidbody.AddForce(Direction * acceleration);
        }
    }
}
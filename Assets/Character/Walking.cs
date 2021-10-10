using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    //! When a character is on the ground, will control its walking in all directions
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
        [Tooltip("when the character stops walking, how fast does he stops actually moving")]
        float decceleration = 1;
        [SerializeField, Min(0)]
        [Tooltip("When the character run at that speed, they can't accelerate anymore")]
        float maxSpeedForward = 2;
        [SerializeField, Min(0)]
        [Tooltip("The speed the character walks from side to side")]
        float speedSide = 1;

        protected virtual void Awake()
        {
            Rigidbody = GetComponentInChildren<Rigidbody>();
            if (speedSide > minSpeedForward) // it shouldn't ba faster to walk side to side than forward, this would cause bugs
                Debug.LogError("SpeedSide greater than minSpeedForward");
        }

        //! Walk the character in the given direction
        public void Walk(CharacterInput input)
        {
            if (input.Direction.sqrMagnitude > 1)
                input.Direction.Normalize();

            Vector3 flatVelocity = Vector3.ProjectOnPlane(Rigidbody.velocity, Vector3.up);
            Vector3 fallVel = Rigidbody.velocity - flatVelocity;

            Vector3 forwardVel = Vector3.Project(Rigidbody.velocity, transform.forward);

            Vector3 forwardInput = Vector3.Project(input.Direction, transform.forward);
            Vector3 sideInput = input.Direction - forwardInput;

            Rigidbody.velocity = MoveForward(forwardInput, forwardVel) 
                + MoveSide(sideInput) 
                + fallVel;
        }

        Vector3 MoveForward(Vector3 input, Vector3 velocity)
        {
            if (Vector3.Dot(input, transform.forward) < 0)
                return MoveSide(input);

            if (velocity.magnitude < minSpeedForward)
                return input * minSpeedForward;
            
            if (input.sqrMagnitude < 0.1f)
                return velocity - velocity * decceleration * Time.deltaTime;

            return velocity + input * acceleration * Time.deltaTime;
        }

        Vector3 MoveSide(Vector3 sideInput)
        {
            return sideInput * speedSide;
        }
    }
}

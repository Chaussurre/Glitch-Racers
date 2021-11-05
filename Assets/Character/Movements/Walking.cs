using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** When a character is on the ground, will control its walking in all directions, when on ground
     * \see GroundDetector
     */
    public class Walking : MonoBehaviour
    {
        new Rigidbody rigidbody;
        GroundDetector ground;

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
        float maxSpeedForward = 10;
        [SerializeField, Min(0)]
        [Tooltip("The speed the character walks from side to side")]
        float speedSide = 1;

        protected virtual void Awake()
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
            ground = GetComponentInChildren<GroundDetector>();
            if (speedSide > minSpeedForward) // it shouldn't ba faster to walk side to side than forward, this would cause bugs
                Debug.LogError("SpeedSide greater than minSpeedForward");
        }

        //! Walk the character in the given direction
        public void Walk(CharacterInput input)
        {
            if (ground && ground.Ground != GroundDetector.GroundType.Walkable) // Only walks on ground
                return;

            input.WalkDirection = Vector3.ProjectOnPlane(input.WalkDirection, Vector3.up);

            if (input.WalkDirection.sqrMagnitude > 1)
                input.WalkDirection.Normalize();

            Vector3 flatVelocity = Vector3.ProjectOnPlane(rigidbody.velocity, Vector3.up);
            Vector3 fallVel = rigidbody.velocity - flatVelocity;

            Vector3 forwardVel = Vector3.Project(rigidbody.velocity, transform.forward);

            Vector3 forwardInput = Vector3.Project(input.WalkDirection, transform.forward);
            Vector3 sideInput = input.WalkDirection - forwardInput;

            rigidbody.velocity = MoveForward(forwardInput, forwardVel) 
                + MoveSide(sideInput) 
                + fallVel;
        }

        Vector3 MoveForward(Vector3 input, Vector3 velocity)
        {
            bool backwardInput = Vector3.Dot(input, transform.forward) < 0;
            bool backwardVel = Vector3.Dot(transform.forward, velocity) < 0;
            
            if (input.sqrMagnitude < 0.1f 
                || (backwardInput && !backwardVel && velocity.sqrMagnitude > 0.1f) 
                || (!backwardInput && backwardVel && velocity.sqrMagnitude > 0.1f))
                return velocity - velocity * decceleration * Time.deltaTime;

            if (backwardInput && velocity.magnitude <= minSpeedForward)
                return MoveSide(input);

            if (velocity.magnitude < minSpeedForward)
                return input * minSpeedForward;

            if (velocity.magnitude < maxSpeedForward)
            {
                Vector3 newVel = velocity + input * acceleration * Time.deltaTime;

                if (newVel.magnitude > maxSpeedForward)
                    newVel = newVel.normalized * maxSpeedForward;

                return newVel;
            }


            return velocity;
        }

        Vector3 MoveSide(Vector3 sideInput)
        {
            return sideInput * speedSide;
        }

    }
}

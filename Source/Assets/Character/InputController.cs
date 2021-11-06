using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    //! All the input necessary to move the character
    public struct CharacterInput
    {
        //! where is the character walking towards
        public Vector3 WalkDirection;
        //! wether the character starts a jump
        public bool Jump;
        //! Holding the jump makes you jump higher
        public bool HoldJump;
        //! Rotation of the Camera
        public Vector3 Camera;
    }

    //!Gives the character Input to MovementManager
    public abstract class InputController : MonoBehaviour
    {
        //! Gives to a character the current input. Should only be called from MovementManager
        public CharacterInput GetInput()
        {
            var input = new CharacterInput();
            input.WalkDirection = GetWalkDirection();
            input.Jump = GetJump(out input.HoldJump);
            input.Camera = GetCameraDirection();
            return input;
        }

        virtual protected Vector3 GetWalkDirection()
        {
            return Vector3.zero;
        }

        virtual protected bool GetJump(out bool hold)
        {
            hold = false;
            return false;
        }

        virtual protected Vector3 GetCameraDirection()
        {
            return Vector3.zero;
        }
    }
}
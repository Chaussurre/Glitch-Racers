using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    //! All the input necessary to move the character
    public struct CharacterInput
    {
        //! where is the character walking towards
        public Vector3 Direction;
    }

    //!Gives the character Input to MovementManager
    public abstract class InputController : MonoBehaviour
    {
        //! Gives to a character the current input. Should only be called from MovementManager
        public CharacterInput GetInput()
        {
            var input = new CharacterInput();
            input.Direction = GetDirection();
            return input;
        }

        virtual protected Vector3 GetDirection()
        {
            return Vector3.zero;
        }
    }
}
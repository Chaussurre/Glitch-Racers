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

    /**Gives the character Input to Movement
     * \see Movement
     * \todo Create AIInput
     */
    public abstract class InputController : MonoBehaviour
    {
        //! Gives to a character the current input. Should only be called from Movement
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
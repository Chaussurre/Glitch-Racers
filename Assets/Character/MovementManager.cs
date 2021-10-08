using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /**This class gets the input from the InputController and redistribute
     * \see InputController \see Walking
     */
    public class MovementManager : MonoBehaviour
    {
        Walking Walker;
        InputController Input;

        private void Awake()
        {
            Walker = GetComponent<Walking>();
            Input = GetComponent<InputController>();
        }

        void Update()
        {
            CharacterInput input = Input.GetInput();

            Walker?.Move(input);
        }
    }
}

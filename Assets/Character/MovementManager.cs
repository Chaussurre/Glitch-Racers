using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \brief This class gets the input from the InputController and redistribute
     * \see InputController \see Walking
     */
    public class MovementManager : MonoBehaviour
    {
        Walking walker;
        GravityApply gravity;
        InputController inputController;

        private void Awake()
        {
            walker = GetComponent<Walking>();
            inputController = GetComponent<InputController>();
            gravity = GetComponent<GravityApply>();
        }

        void Update()
        {
            CharacterInput input = inputController.GetInput();

            walker?.Walk(input);
            gravity?.GravityPush();
        }
    }
}

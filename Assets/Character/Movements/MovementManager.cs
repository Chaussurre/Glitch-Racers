using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /** \brief This class gets the input from the InputController and redistribute
     * \see InputController \see Walking \see GravityApply
     */
    public class MovementManager : MonoBehaviour
    {
        Walking walker;
        GravityApply gravity;
        InputController inputController;
        CharacterRotation rotation;

        private void Awake()
        {
            walker = GetComponentInChildren<Walking>();
            inputController = GetComponent<InputController>();
            gravity = GetComponentInChildren<GravityApply>();
            rotation = GetComponentInChildren<CharacterRotation>();
        }

        void Update()
        {
            CharacterInput input = inputController.GetInput();

            walker?.Walk(input);
            //rotation?.LookAround(input);
            gravity?.GravityPush();
        }
    }
}

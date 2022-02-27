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
        Jump jump;
        WallRunner wallRunner;
        WallClimber wallClimber;
        LedgeCatcher LedgeCatcher;
        GrapplingHookShooter hookShooter;

        private void Awake()
        {
            walker = GetComponentInChildren<Walking>();
            inputController = GetComponent<InputController>();
            gravity = GetComponentInChildren<GravityApply>();
            rotation = GetComponentInChildren<CharacterRotation>();
            jump = GetComponentInChildren<Jump>();
            wallRunner = GetComponentInChildren<WallRunner>();
            wallClimber = GetComponentInChildren<WallClimber>();
            LedgeCatcher = GetComponentInChildren<LedgeCatcher>();
            hookShooter = GetComponentInChildren<GrapplingHookShooter>();
        }

        void Update()
        {
            CharacterInput input = inputController.GetInput();

            hookShooter.tryShoot(input);
            walker?.Walk(input);
            jump?.TryJump(input);
            wallRunner?.TryWallRun();
            wallClimber?.TryWallClimb();
            gravity?.GravityPush();
            rotation?.LookAround(input);
            LedgeCatcher?.TryCatchLedge(input);
            rotation.CheckCollision();
        }
    }
}

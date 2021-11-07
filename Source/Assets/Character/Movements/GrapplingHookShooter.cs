using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHookShooter : MonoBehaviour
    {
        [SerializeField]
        Transform viewPoint;
        GrapplingHook grapplingHook;

        public bool IsHooking { get { return grapplingHook.Hooking; } }

        private void Start()
        {
            grapplingHook = GetComponentInChildren<GrapplingHook>();
        }

        public void tryShoot(CharacterInput input)
        {
            if (input.Hook)
            {
                if (!grapplingHook.Hooking && Physics.Raycast(viewPoint.position, viewPoint.forward, out RaycastHit hit))
                    grapplingHook.Hook(hit);
            }
            else
                grapplingHook.ResetHook();

            grapplingHook.UpdqteHook(input);
        }
    }
}
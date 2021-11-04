using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class LedgeCatcher : MonoBehaviour
    {
        [SerializeField]
        bool CatchedLedge;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void TryCatchLedge()
        {
            if (CatchedLedge)
                rb.velocity = Vector3.zero;
        }
    }
}

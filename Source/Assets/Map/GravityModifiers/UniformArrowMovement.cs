using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.GravityModify
{

    public class UniformArrowMovement : MonoBehaviour
    {
        [Tooltip("The speed of movements of the arrows")]
        [SerializeField, Min(0)]
        float Speed = 0;

        float pos = 0;
        private void Update()
        {
            pos += Speed * Time.deltaTime;
            pos = pos % .1f;

            transform.localPosition = Vector3.down * pos;
        }
    }
}

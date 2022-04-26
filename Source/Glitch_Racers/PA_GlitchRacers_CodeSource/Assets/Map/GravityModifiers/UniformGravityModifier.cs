using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.GravityModify
{
    public class UniformGravityModifier : GravityModifier
    {
        [SerializeField]
        Vector3 Direction;

        public override Vector3 GetGravityDirection(Vector3 position)
        {
            return Direction;
        }
    }
}

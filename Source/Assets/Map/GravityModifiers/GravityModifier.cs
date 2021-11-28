using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.GravityModify
{
    /**Define a zone where gravity doesn't act the same
     * 
     */
    public abstract class GravityModifier : MonoBehaviour
    {
        abstract public Vector3 GetGravityDirection(Vector3 position);
    }
}
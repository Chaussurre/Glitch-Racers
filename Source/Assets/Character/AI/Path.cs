using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character.IA
{
    public class Path : MonoBehaviour
    {
        
        [SerializeField] private List<PathTarget> Targets = new List<PathTarget>();
        private int incrementI;
        private bool finish = true;

        public Vector3 getNextTarget(GameObject body)
        {
            PathTarget target = Targets[incrementI];
            if (Vector3.Distance(body.transform.position, target.transform.position) < 1)
            {
                incrementI++;
                if (incrementI == Targets.Count)
                {
                    if (finish == false)
                    {
                        return target.transform.position;
                    }
                    incrementI = 0;
                    Targets.Reverse();
                    finish = false;
                }
            }

            return target.transform.position;
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (Targets.Count > 1)
            {
                Gizmos.color = Color.red;
                for (int i = 1; i < Targets.Count; i++)
                {
                    Gizmos.DrawLine(Targets[i].transform.position, Targets[i-1].transform.position);
                }
                Gizmos.DrawLine(Targets[0].transform.position, Targets[Targets.Count-1].transform.position);
            }
            
        }
#endif
    }
}


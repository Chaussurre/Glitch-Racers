using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GrapplingHookDisplay : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The transform that will hold the end of the grappling hook")]
        Transform hand;

        [SerializeField]
        GameObject StraightHookDisplay;

        GrapplingHook grapplingHook;

        [SerializeField]
        GrapplingHookSegment segmentPrefab;
        [SerializeField, Min(0)]
        float DistanceSegment;

        readonly List<GrapplingHookSegment> Segments = new List<GrapplingHookSegment>();

        // Start is called before the first frame update
        void Start()
        {
            grapplingHook = GetComponent<GrapplingHook>();
            for (int i = 0; i < 300; i++)
            {
                Segments.Add(Instantiate(segmentPrefab, transform));
                Segments[i].gameObject.SetActive(false);

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (grapplingHook.Hooking)
            {
                if (grapplingHook.IsAtMaxDistance)
                    MaxRangeDisplay();
                else
                    InverseCinematicDislpay();
            }
            else
            {
                foreach (var seg in Segments)
                    seg.gameObject.SetActive(false);
                StraightHookDisplay.SetActive(false);
            }
        }

        void MaxRangeDisplay()
        {
            Vector3 relativHandPos = hand.position - grapplingHook.HookedPoint;

            for (int i = 0; i < Segments.Count; i++)
                Segments[i].gameObject.SetActive(false);
            
            int count = Mathf.CeilToInt(grapplingHook.MaxDistance / DistanceSegment);
            count = Mathf.Min(count, Segments.Count);

            for (int i = 0; i < count; i++)
            {
                Segments[i].transform.position = Vector3.Lerp(grapplingHook.HookedPoint, hand.position, i / (float)count);
                Segments[i].transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, relativHandPos), relativHandPos);
            }


            StraightHookDisplay.SetActive(true);

            StraightHookDisplay.transform.position = Vector3.Lerp(grapplingHook.HookedPoint, hand.position, 0.5f);
            StraightHookDisplay.transform.localScale = new Vector3(.1f, relativHandPos.magnitude / 2, .1f);
            StraightHookDisplay.transform.rotation = Quaternion.LookRotation(Vector3.Cross(relativHandPos, Vector3.left), relativHandPos);
        }

        void InverseCinematicDislpay()
        {
            StraightHookDisplay.SetActive(false);

            if (DistanceSegment <= 0) Debug.LogError($"Distance segment must be strictly positive (value : {DistanceSegment})");
            int count = Mathf.CeilToInt(grapplingHook.MaxDistance / DistanceSegment);
            count = Mathf.Min(count, Segments.Count);

            var last = grapplingHook.HookedPoint;
            for (int i = count - 1; i >= 0; i--)
            {
                Segments[i].gameObject.SetActive(true);
                last = Segments[i].SetAnchorToPoint(last, AnchorIsStart: true);
            }
            
            last = hand.position;
            for (int i = 0; i < count; i++)
                last = Segments[i].SetAnchorToPoint(last, AnchorIsStart: false);
            
            for (int i = count; i < Segments.Count; i++)
                Segments[i].gameObject.SetActive(false);
        }
    }
}

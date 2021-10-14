using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit;
using UnityEngine.TestTools;
using NUnit.Framework;
using Character;

namespace Tests
{
    public class GroundDetectorTests
    {
        public static GroundDetector SetUpGroundDetector(out Rigidbody rigidbody, GameObject go = null)
        {
            if (go == null)
                go = new GameObject();

            if (!go.TryGetComponent(out rigidbody))
                rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            go.AddComponent<SphereCollider>();
            GroundDetector detector = go.AddComponent<GroundDetector>();
            return detector;
        }

        [UnityTest]
        public IEnumerator GroundDetectorNoGroundTest()
        {
            var detector = SetUpGroundDetector(out Rigidbody rigidbody);
            
            yield return null;

            Assert.IsFalse(detector.OnGround);
        }

        [UnityTest]
        public IEnumerator GroundDetectorOnGroundTest()
        {
            var detector = SetUpGroundDetector(out Rigidbody rigidbody);
            detector.gameObject.AddComponent<GravityApply>();

            var ground = new GameObject();
            ground.AddComponent<BoxCollider>();
            ground.transform.position = Vector3.down;

            yield return new WaitForSeconds(1);

            Assert.IsTrue(detector.OnGround);
        }
    }
}

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Character;
using System.Reflection;

namespace Tests
{
    //! Test class for Character.MovementManager
    public class MovementManagerTests
    {
        class TestInput : InputController
        {
            public static Vector3 direction;
            protected override Vector3 GetWalkDirection()
            {
                return direction;
            }
        }

        public static MovementManager SetUpMovement(float speed, out Rigidbody rigidbody)
        {
            GameObject go = new GameObject();
            go.AddComponent<TestInput>();

            rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            WalkingTests.SetUpWalking(speed, out rigidbody, go);

            return go.AddComponent<MovementManager>();
        }

        [UnityTest]
        public IEnumerator MovementManagerWalkTest()
        {
            var mov = SetUpMovement(1, out Rigidbody rigidbody);
            TestInput.direction = Vector3.forward;

            yield return null;

            Assert.AreEqual(Vector3.forward, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator MovementManagerNotMovingTest()
        {
            SetUpMovement(1, out Rigidbody rigidbody);
            TestInput.direction = Vector3.zero;
            yield return null;

            Assert.AreEqual(Vector3.zero, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator MovementManagerSpeedTest()
        {
            SetUpMovement(3, out Rigidbody rigidbody);
            TestInput.direction = Vector3.forward;
            yield return null;

            Assert.AreEqual(Vector3.forward * 3, rigidbody.velocity);
        }
    }
}
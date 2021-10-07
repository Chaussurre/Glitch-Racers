using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Character;
using System.Reflection;

namespace Tests
{
    public class MovementTests
    {
        public class TestInput : InputController
        {
            public static Vector3 direction;
            protected override Vector3 GetDirection()
            {
                return direction;
            }
        }

        public static Movement SetUpMovement(float speed, out Rigidbody rigidbody)
        {
            GameObject go = new GameObject();
            go.AddComponent<TestInput>();

            rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            Movement movement = go.AddComponent<Movement>();
            var field = typeof(Movement).GetField("speedForward",
                BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(movement, speed);
            return movement;
        }

        [UnityTest]
        public IEnumerator MovementUnitTest()
        {
            var move = SetUpMovement(1, out Rigidbody rigidbody);
            TestInput.direction = Vector3.forward;
            yield return null;

            Assert.AreEqual(Vector3.forward, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator MovementNotMovingTest()
        {
            var move = SetUpMovement(1, out Rigidbody rigidbody);
            TestInput.direction = Vector3.zero;
            yield return null;

            Assert.AreEqual(Vector3.zero, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator MovementSpeedTest()
        {
            var move = SetUpMovement(3, out Rigidbody rigidbody);
            TestInput.direction = Vector3.forward;
            yield return null;

            Assert.AreEqual(Vector3.forward * 3, rigidbody.velocity);
        }
    }
}
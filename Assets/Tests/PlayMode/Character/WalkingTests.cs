using Character;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WalkingTests
    {
        public static Walking SetUpWalking(float speed, out Rigidbody rigidbody, GameObject go = null)
        {
            if (go == null)
                go = new GameObject();
            if(!go.TryGetComponent(out rigidbody))
                rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            Walking movement = go.AddComponent<Walking>();
            var field = typeof(Walking).GetField("speedForward",
                BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(movement, speed);
            return movement;
        }

        [UnityTest]
        public IEnumerator WalkingUnitTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Move(new CharacterInput() { Direction = Vector3.forward});
            
            yield return null;

            Assert.AreEqual(Vector3.forward, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingNotMovingTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Move(new CharacterInput() { Direction = Vector3.zero });

            yield return null;

            Assert.AreEqual(Vector3.zero, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingSpeedTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);
            move.Move(new CharacterInput() { Direction = Vector3.forward });
            yield return null;

            Assert.AreEqual(Vector3.forward * 3, rigidbody.velocity);
        }
    }
}

using Character;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    //! Test class for Character.Walking
    public class WalkingTests
    {
        public static Walking SetUpWalking(float minSpeedForward, out Rigidbody rigidbody, GameObject go = null)
        {
            if (go == null)
                go = new GameObject();
            if(!go.TryGetComponent(out rigidbody))
                rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            Walking movement = go.AddComponent<Walking>();
            var field = typeof(Walking).GetField("minSpeedForward",
                BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(movement, minSpeedForward);
            return movement;
        }

        [UnityTest]
        public IEnumerator WalkingUnitTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Walk(new CharacterInput() { Direction = Vector3.forward });

            yield return null;

            Assert.AreEqual(Vector3.forward, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingWrongInputTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Walk(new CharacterInput() { Direction = Vector3.forward * 10 });

            yield return null;

            Assert.AreEqual(Vector3.forward, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingHalfInputTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Walk(new CharacterInput() { Direction = Vector3.forward * 0.5f });

            yield return null;

            Assert.AreEqual(Vector3.forward * 0.5f, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingNotMovingTest()
        {
            var move = SetUpWalking(1, out Rigidbody rigidbody);
            move.Walk(new CharacterInput() { Direction = Vector3.zero });

            yield return null;

            Assert.AreEqual(Vector3.zero, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingSpeedTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);
            move.Walk(new CharacterInput() { Direction = Vector3.forward });
            yield return null;

            Assert.AreEqual(Vector3.forward * 3, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingBackwardTest()
        {
            var move = SetUpWalking(10, out Rigidbody rigidbody);

            typeof(Walking).GetField("speedSide",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 1);

            move.Walk(new CharacterInput() { Direction = Vector3.back });

            yield return null;

            Assert.AreEqual(Vector3.back, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingAccelerateTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);

            typeof(Walking).GetField("acceleration",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 1);

            for (int i = 0; i < 10; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.forward });
                yield return null;
            }

            Debug.Log("velocity : " + rigidbody.velocity);
            Assert.IsTrue(rigidbody.velocity.z > 3.0f);
        }

        [UnityTest]
        public IEnumerator WalkingAccelerateMaxTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);

            typeof(Walking).GetField("acceleration",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 100);

            typeof(Walking).GetField("maxSpeedForward",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 10);

            for (int i = 0; i < 20; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.forward });
                yield return null;
            }

            Assert.AreEqual(Vector3.forward * 10, rigidbody.velocity);
        }

        [UnityTest]
        public IEnumerator WalkingForwardThenBackTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);

            typeof(Walking).GetField("acceleration",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 100);

            for (int i = 0; i < 10; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.forward });
                yield return null;
            }

            for (int i = 0; i < 3; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.back });
                yield return null;
            }

            Debug.Log(rigidbody.velocity);
            Assert.IsTrue(rigidbody.velocity.z > 0);
        }
        
        [UnityTest]
        public IEnumerator WalkingDeccelerateTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);

            typeof(Walking).GetField("acceleration",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 1);

            for (int i = 0; i < 10; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.forward });
                yield return null;
            }

            for (int i = 0; i < 10; i++)
            {
                move.Walk(new CharacterInput() { Direction = Vector3.zero });
                yield return null;
            }

            Debug.Log("velocity : " + rigidbody.velocity);
            Assert.IsTrue(rigidbody.velocity.z < 3.0f);
        }

        [UnityTest]
        public IEnumerator WalkingSideStepNotAccelerateTest()
        {
            var move = SetUpWalking(3, out Rigidbody rigidbody);

            typeof(Walking).GetField("speedSide",
                BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(move, 1);

            move.Walk(new CharacterInput() { Direction = Vector3.left });
            move.Walk(new CharacterInput() { Direction = Vector3.left });
            move.Walk(new CharacterInput() { Direction = Vector3.left });

            yield return null;

            Assert.AreEqual(Vector3.left, rigidbody.velocity);
        }
    }
}

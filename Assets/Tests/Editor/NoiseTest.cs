//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using NUnit.Framework;
using UnityEngine;

namespace Lumpn.WFC.Tests
{
    [TestFixture]
    public sealed class NoiseTest
    {
        [Test]
        public void Test1()
        {
            var noise = new Noise3(1);
            var value = noise.Range(Vector3Int.zero, 0, 1);

            Assert.AreEqual(0, value);
        }

        [Test]
        public void Test2()
        {
            var noise = new Noise3(1);
            var value = noise.Range(Vector3Int.zero, 0, 100);

            Assert.AreEqual(0, value);
        }
    }
}

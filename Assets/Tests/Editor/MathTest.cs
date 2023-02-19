//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using NUnit.Framework;
using Unity.Mathematics;

namespace Lumpn.WFC.Tests
{
    [TestFixture]
    public sealed class MathTest
    {
        [Test]
        public void TrailingZerosCount()
        {
            Assert.AreEqual(0, math.tzcnt(1UL << 0));
            Assert.AreEqual(1, math.tzcnt(1UL << 1));
            Assert.AreEqual(5, math.tzcnt(1UL << 5));
        }
    }
}

//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using NUnit.Framework;
using Unity.Burst.Intrinsics;

namespace Lumpn.WFC.Tests
{
    [TestFixture]
    public sealed class IntrinsicsTest
    {
        [Test]
        public void PopCount()
        {
            Assert.AreEqual(0, X86.Popcnt.popcnt_u64(0UL));
            Assert.AreEqual(1, X86.Popcnt.popcnt_u64(1UL));
            Assert.AreEqual(1, X86.Popcnt.popcnt_u64(1UL << 3));
            Assert.AreEqual(2, X86.Popcnt.popcnt_u64(3UL));
        }
    }
}

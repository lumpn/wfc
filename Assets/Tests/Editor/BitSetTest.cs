//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using NUnit.Framework;

namespace Lumpn.WFC.Tests
{
    [TestFixture]
    public sealed class BitSetTest
    {
        [Test]
        public void EnumerateEmpty()
        {
            var set = new BitSet();
            var enumerator = set.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void EnumerateSingle()
        {
            var set = new BitSet(1UL << 0);
            var enumerator = set.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void EnumerateHighSingle()
        {
            var set = new BitSet(1UL << 50);
            var enumerator = set.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}

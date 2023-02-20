//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

namespace Lumpn.WFC
{
    public struct BitSet : IEnumerable<int>
    {
        private ulong bits;

        public ulong value => bits;

        public BitSet(ulong bits)
        {
            this.bits = bits;
        }

        public int Count()
        {
            return X86.Popcnt.popcnt_u64(bits);
        }

        public int First()
        {
            return math.tzcnt(bits);
        }

        public bool IntersectWith(BitSet other)
        {
            var oldValue = bits;
            bits &= other.bits;

            AssertFormat(bits != 0, "Contradiction (oldValue {0}, other {1}, intersection 0)", oldValue, other.bits);
            return (oldValue != bits);
        }

        public void UnionWith(BitSet other)
        {
            bits |= other.bits;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new BitSetEnumerator(bits);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void AssertFormat(bool condition, string format, ulong p1, ulong p2)
        {
            if (!condition)
            {
                Debug.LogAssertionFormat(format, p1, p2);
            }
        }
    }
}

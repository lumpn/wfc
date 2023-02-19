//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Lumpn.WFC
{
    public struct BitSet : IEnumerable<int>
    {
        private ulong bits;

        public int Count()
        {
            return X86.Popcnt.popcnt_u64(bits);
        }

        public bool IntersectWith(BitSet other)
        {
            var oldValue = bits;
            bits &= other.bits;

            Debug.Assert(bits != 0, "Contradiction");
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
    }
}

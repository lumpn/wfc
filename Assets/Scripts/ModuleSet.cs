using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;

namespace Lumpn.WFC
{
    public struct ModuleSet
    {
        private ulong ids;

        [Unity.Burst.BurstDiscardAttribute]
        public int Count()
        {
            return X86.Popcnt.popcnt_u64(ids);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Lumpn.WFC
{
    public struct ModuleSet : IEnumerable<int>
    {
        private ulong ids;

        public int Count()
        {
            return X86.Popcnt.popcnt_u64(ids);
        }

        public bool Constrain(ModuleSet allowed)
        {
            var oldValue = ids;
            ids &= allowed.ids;

            Debug.Assert(ids != 0, "Contradiction");
            return (oldValue != ids);
        }

        public void Add(ModuleSet allowed)
        {
            ids |= allowed.ids;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new ModuleSetEnumerator(ids);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System.Collections;

namespace Lumpn.WFC
{
    public struct BitSetEnumerator : IEnumerator<int>
    {
        private const int numBits = sizeof(ulong) * 8;

        private readonly ulong bits;
        private int index;

        public BitSetEnumerator(ulong ids)
        {
            this.bits = ids;
            this.index = -1;
            // TODO Jonas: skip trailing zeros, stop at leading zeros?
        }

        public int Current => index;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (index < numBits)
            {
                index++;

                if ((bits & (1UL << index)) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}

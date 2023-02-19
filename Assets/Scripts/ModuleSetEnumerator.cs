using System.Collections.Generic;
using System.Collections;

namespace Lumpn.WFC
{
    public struct ModuleSetEnumerator : IEnumerator<int>
    {
        private const int numBits = sizeof(ulong) * 8;

        private readonly ulong ids;
        private int bit;

        public ModuleSetEnumerator(ulong ids)
        {
            this.ids = ids;
            this.bit = -1;
        }

        public int Current => bit;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (bit < numBits)
            {
                bit++;

                if ((ids & (1UL << bit)) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            bit = -1;
        }
    }
}

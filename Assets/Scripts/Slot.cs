//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    // wave function of modules
    public sealed class Slot
    {
        public readonly Vector3Int position;
        private readonly SlotType type;
        private BitSet candidates;
        private bool isDirty = false;

        public Slot(Vector3Int position, SlotType type)
        {
            this.position = position;
            this.type = type;
            this.candidates = new BitSet(type.candidates);
        }

        public bool Constrain(BitSet allowed)
        {
            return candidates.IntersectWith(allowed);
        }

        public void MarkClean()
        {
            isDirty = false;
        }

        public bool MarkDirty()
        {
            var result = !isDirty;
            isDirty = true;
            return result;
        }

        public BitSet GetAllowed(Direction direction)
        {
            var result = new BitSet();
            foreach (int id in candidates)
            {
                var module = type.modules[id];
                var allowed = module.GetAllowed(direction);
                result.UnionWith(allowed);
            }
            return result;
        }

        public void Spawn(Transform parent, Vector3Int position)
        {
            Debug.Assert(candidates.Count() == 1, "Not collapsed");
            var id = candidates.First();

            var module = type.modules[id];
            module.Spawn(parent, position);
        }

        public bool IsOpen()
        {
            return (candidates.Count() > 1);
        }

        public int GetEntropy()
        {
            // TODO Jonas: entropy
            return candidates.Count();
        }

        public void Collapse(IRandom random)
        {
            // TODO Jonas: weighted selection
            var rank = random.Range(position, 0, candidates.Count());

            var enumerator = candidates.GetEnumerator();
            enumerator.MoveNext();
            for (int i = 0; i < rank; i++)
            {
                enumerator.MoveNext();
            }

            var id = enumerator.Current;

            //Debug.LogFormat("candidates {0}, num candidates {1}, rank {2}, id {3}", candidates.value, candidates.Count(), rank, id);

            candidates.IntersectWith(new BitSet(1UL << id));
            Debug.Assert(candidates.Count() == 1, "Not collapsed");
        }
    }
}

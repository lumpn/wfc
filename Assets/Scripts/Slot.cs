//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System;
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

        public void Spawn(Vector3Int position)
        {
            Debug.Assert(candidates.Count() == 1, "Not collapsed");
            var id = candidates.First();

            var module = type.modules[id];
            module.Spawn(position);
        }

        public bool IsOpen()
        {
            return (candidates.Count() > 1);
        }

        public int GetEntropy()
        {
            return candidates.Count();
        }

        public void Collapse()
        {
            var id = candidates.First(); // TODO Jonas: randomize
            candidates.IntersectWith(new BitSet(1UL << id));
            Debug.Assert(candidates.Count() == 1, "Not collapsed");
        }
    }
}

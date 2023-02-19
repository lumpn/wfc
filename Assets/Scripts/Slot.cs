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
        private readonly SlotType type;
        private BitSet candidates;
        private bool isDirty = false;

        public Slot(SlotType type)
        {
            this.type = type;
            this.candidates = type.candidates;
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
        }
    }
}

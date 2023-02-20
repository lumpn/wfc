//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    public sealed class Wave
    {
        private readonly Level level;

        private readonly Queue<Slot> wave = new Queue<Slot>();

        public Wave(Level level)
        {
            this.level = level;
        }

        public void Collapse()
        {
            var openSlots = new List<Slot>(level.GetOpenSlots());

            while (openSlots.Count > 0)
            {
                openSlots.Sort(CompareByEntropy);
                var slot = openSlots[openSlots.Count - 1];
                openSlots.RemoveAt(openSlots.Count - 1);

                slot.Collapse();
                wave.Enqueue(slot);
                Process();
            }
        }

        private static int CompareByEntropy(Slot a, Slot b)
        {
            return a.GetEntropy() - b.GetEntropy();
        }

        public void Constrain(Vector3Int position, BitSet allowed)
        {
            if (level.TryGetSlot(position, out Slot slot))
            {
                Constrain(slot, allowed);
            }
        }

        public void Process()
        {
            while (wave.TryDequeue(out Slot slot))
            {
                var position = slot.position;
                slot.MarkClean();

                foreach (var direction in DirectionUtils.directions)
                {
                    var neighborPosition = level.GetNeighbor(position, direction);
                    if (level.TryGetSlot(neighborPosition, out Slot neighbor))
                    {
                        var allowed = slot.GetAllowed(direction);
                        Constrain(neighbor, allowed);
                    }
                }
            }
        }

        private void Constrain(Slot slot, BitSet allowed)
        {
            var changed = slot.Constrain(allowed);

            if (changed)
            {
                // propagate
                if (slot.MarkDirty())
                {
                    wave.Enqueue(slot);
                }
            }
        }
    }
}

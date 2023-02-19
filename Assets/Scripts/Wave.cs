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
        private static readonly Direction[] directions =
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.Up,
            Direction.Down,
        };

        private readonly Level level;

        private readonly Queue<System.ValueTuple<Vector3Int, Slot>> wave = new Queue<System.ValueTuple<Vector3Int, Slot>>();

        public Wave(Level level)
        {
            this.level = level;
        }

        public void Collapse()
        {
            // TODO Jonas: collapse all non-collapsed slots
        }

        public void Constrain(Vector3Int position, BitSet allowed)
        {
            if (level.TryGetSlot(position, out Slot slot))
            {
                Constrain(position, slot, allowed);
            }
        }

        public void Process()
        {
            while (wave.TryDequeue(out System.ValueTuple<Vector3Int, Slot> pair))
            {
                var position = pair.Item1;
                var slot = pair.Item2;
                slot.MarkClean();

                foreach (var direction in directions)
                {
                    var neighborPosition = level.GetNeighbor(position, direction);
                    if (level.TryGetSlot(neighborPosition, out Slot neighbor))
                    {
                        var allowed = slot.GetAllowed(direction);
                        Constrain(neighborPosition, neighbor, allowed);
                    }
                }
            }
        }

        private void Constrain(Vector3Int position, Slot slot, BitSet allowed)
        {
            var changed = slot.Constrain(allowed);

            if (changed)
            {
                // propagate
                if (slot.MarkDirty())
                {
                    wave.Enqueue(new System.ValueTuple<Vector3Int, Slot>(position, slot));
                }
            }
        }
    }
}

//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    // collection of slots
    public sealed class Level
    {
        private static readonly Dictionary<Direction, Vector3Int> offsets = new Dictionary<Direction, Vector3Int>
        {
            {Direction.North, Vector3Int.forward},
            {Direction.South, Vector3Int.back},
            {Direction.East, Vector3Int.right},
            {Direction.West, Vector3Int.left},
            {Direction.Up, Vector3Int.up},
            {Direction.Down, Vector3Int.down},
        };

        private readonly Vector3Int size;
        private readonly Slot[,,] slots;

        public Level(Vector3Int size, SlotType[] slotTypes)
        {
            this.size = size;
            this.slots = new Slot[size.x, size.y, size.z];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        var slotType = slotTypes[(x % 3) + (z % 3) * 3 + (y % 3) * 3 * 3];
                        slots[x, y, z] = new Slot(new Vector3Int(x, y, z), slotType);
                    }
                }
            }
        }

        public void Spawn(Transform parent)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        var slot = slots[x, y, z];
                        var position = new Vector3Int(x, z, y);
                        slot.Spawn(parent, position);
                    }
                }
            }
        }

        public bool TryGetSlot(Vector3Int position, out Slot slot)
        {
            if ((position.x >= 0 && position.x < size.x) &&
                (position.y >= 0 && position.y < size.y) &&
                (position.z >= 0 && position.z < size.z))
            {
                slot = slots[position.x, position.y, position.z];
                return true;
            }

            slot = null;
            return false;
        }

        public Vector3Int GetNeighbor(Vector3Int position, Direction direction)
        {
            var offset = offsets[direction];
            return position + offset;
        }

        public IEnumerable<Slot> GetOpenSlots()
        {
            foreach (var slot in slots)
            {
                if (slot.IsOpen())
                {
                    yield return slot;
                }
            }
        }
    }
}

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

        private readonly Slot[,,] slots;

        public Level(Vector3Int size)
        {
            slots = new Slot[size.x, size.y, size.z];
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        slots[x, y, z] = new Slot();
                    }
                }
            }
        }

        public Slot GetSlot(Vector3Int position)
        {
            return slots[position.x, position.y, position.z];
        }

        public Vector3Int GetNeighbor(Vector3Int position, Direction direction)
        {
            var offset = offsets[direction];
            return position + offset;
        }
    }
}

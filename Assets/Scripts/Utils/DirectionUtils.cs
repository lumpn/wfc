//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------

namespace Lumpn.WFC
{
    public static class DirectionUtils
    {
        public static readonly Direction[] directions =
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.Up,
            Direction.Down,
        };

        public static readonly Direction[] inverseDirections =
        {
            Direction.South,
            Direction.North,
            Direction.West,
            Direction.East,
            Direction.Down,
            Direction.Up,
        };
    }
}

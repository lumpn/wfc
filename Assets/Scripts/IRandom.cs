//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    public interface IRandom
    {
        int Range(Vector3Int position, int min, int max);
    }
}

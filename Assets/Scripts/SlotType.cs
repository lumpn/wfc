//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;

namespace Lumpn.WFC
{
    // type of slot, e.g. volume, wall, edge, corner, rim, rail
    public class SlotType : ScriptableObject
    {
        public Module[] modules;
        public BitSet allowed;

        // TODO implement
    }
}

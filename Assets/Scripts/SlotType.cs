//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;

namespace Lumpn.WFC
{
    // type of slot, e.g. volume, wall, edge, corner, rim, rail
    [CreateAssetMenu]
    public class SlotType : ScriptableObject
    {
        public Module[] modules;
        public BitSet candidates;

        // TODO Jonas: implement
    }
}

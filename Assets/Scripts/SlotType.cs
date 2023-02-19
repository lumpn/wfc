//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

namespace Lumpn.WFC
{
    // type of slot, e.g. volume, wall, edge, corner, rim, rail
    [CreateAssetMenu]
    public class SlotType : ScriptableObject
    {
        [SerializeField] public List<Module> modules;
        [SerializeField] public BitSet candidates;

        // TODO Jonas: implement
    }
}

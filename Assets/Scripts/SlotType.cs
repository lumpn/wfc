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
        [SerializeField] public SlotType[] neighbors;
        [SerializeField] public List<Module> modules;
        [SerializeField] public BitSet candidates;

        [ContextMenu("Propagate")]
        private void Propagate()
        {
            foreach (var direction in DirectionUtils.directions)
            {
                var neighborSlotType = neighbors[(int)direction];
                var inverseDirection = DirectionUtils.inverseDirections[(int)direction];
                neighborSlotType.neighbors[(int)inverseDirection] = this;
            }
        }
    }
}

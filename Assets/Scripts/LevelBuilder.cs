//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;

namespace Lumpn.WFC
{
    public sealed class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Vector3Int size;
        [SerializeField] private SlotType[] slotTypes;
        [SerializeField] private int interiorVolumeId;

        protected void Start()
        {
            var level = new Level(size, slotTypes);
            var wave = new Wave(level);

            //wave.Constrain(Vector3Int.one, new BitSet(1UL << interiorVolumeId));
            //wave.Process();
            //wave.Collapse();

            level.Spawn();
        }
    }
}

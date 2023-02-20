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
        [SerializeField] private Module interiorVolume;
        [SerializeField] private Module exteriorVolume;
        [SerializeField] private int seed;

        protected void Start()
        {
            var level = new Level(size, slotTypes);
            var wave = new Wave(level);

            wave.Constrain(Vector3Int.one, new BitSet(1UL << exteriorVolume.id));
            wave.Process();

            var random = new Noise3(seed);
            wave.Collapse(random);

            level.Spawn();
        }
    }
}

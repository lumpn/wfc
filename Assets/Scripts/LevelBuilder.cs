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

        [System.NonSerialized] private GameObject root;

        protected void Start()
        {
            Build();
        }

        [ContextMenu(nameof(Build))]
        public void Build()
        {
            var level = new Level(size, slotTypes);
            var wave = new Wave(level);

            wave.Constrain(Vector3Int.one, new BitSet(1UL << exteriorVolume.id));
            wave.Process();

            var random = new Noise3(seed);
            wave.Collapse(random);

            Object.Destroy(root);
            root = new GameObject($"Level{seed}");
            level.Spawn(root.transform);
        }

        [ContextMenu(nameof(Next))]
        public void Next()
        {
            seed++;
            Start();
        }
    }
}

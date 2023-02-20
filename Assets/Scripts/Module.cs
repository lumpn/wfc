//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    public sealed class Module : MonoBehaviour
    {
        [SerializeField] public int id;
        [SerializeField] public SlotType slotType;
        [SerializeField] public Prototype prototype;
        [SerializeField] public Connector[] connectors;
        [SerializeField] public ulong[] allowed;

        public BitSet GetAllowed(Direction direction)
        {
            return new BitSet(allowed[(int)direction]);
        }

        public void Spawn(Vector3Int position)
        {
            var worldPosition = Vector3Int.Scale(position / 3, new Vector3Int(5, 4, 5));
            Object.Instantiate(gameObject, worldPosition, Quaternion.identity);
        }
    }
}

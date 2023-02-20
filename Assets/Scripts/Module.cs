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
        [SerializeField] public Prototype prototype;
        [SerializeField] public ulong[] allowed;

        public BitSet GetAllowed(Direction direction)
        {
            return new BitSet(allowed[(int)direction]);
        }

        public void Spawn(Vector3Int position)
        {
            // TODO Jonas: implement scaling
            Object.Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}

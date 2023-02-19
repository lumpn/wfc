//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;

namespace Lumpn.WFC
{
    public sealed class Module : MonoBehaviour
    {
        public BitSet GetAllowed(Direction direction)
        {
            // TODO Jonas: implement
            return new BitSet(0);
        }

        public void Spawn(Vector3Int position)
        {
            // TODO Jonas: implement scaling
            Object.Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}

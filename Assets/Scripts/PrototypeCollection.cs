//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    [CreateAssetMenu]
    public class PrototypeCollection : ScriptableObject
    {
        [SerializeField] public Prototype[] prototypes;
    }
}

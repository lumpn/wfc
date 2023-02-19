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
        [Header("Slot rotations")]
        [SerializeField] public Vector3[] positions;
        [SerializeField] public Quaternion[] rotations;

        [SerializeField] public Prototype[] prototypes;
    }
}

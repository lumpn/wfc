//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEngine;

namespace Lumpn.WFC
{
    [CreateAssetMenu]
    public class ModuleGenerator : ScriptableObject
    {
        [Header("Slot rotations")]
        [SerializeField] public Vector3[] positions;
        [SerializeField] public Quaternion[] rotations;

        [SerializeField] public SlotType[] slotTypes;
        [SerializeField] public PrototypeCollection prototypeCollection;
    }
}

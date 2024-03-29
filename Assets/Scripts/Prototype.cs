//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    // unrotated prototype for modules
    public class Prototype : MonoBehaviour
    {
        [SerializeField] public PrototypeSlot slot;
        [SerializeField] public Connector[] connectors;
    }
}

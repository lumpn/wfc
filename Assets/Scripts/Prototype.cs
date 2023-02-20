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
        [SerializeField] public Connector north, south, east, west, up, down;
        [SerializeField] public Connector[] connectors;


        [ContextMenu(nameof(ConvertConnectors))]
        public void ConvertConnectors()
        {
            connectors = new[]
            {
                north,
                south,
                east,
                west,
                up,
                down,
            };
        }
    }
}

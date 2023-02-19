using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    // type of slot, e.g. volume, wall, edge, corner, rim, rail
    public class SlotType : ScriptableObject
    {
        public Module[] modules;
        public ModuleSet allowed;

        // TODO implement
    }
}

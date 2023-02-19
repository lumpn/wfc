using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    // wave function of modules
    public sealed class Slot 
    {
        private bool isDirty = false;

        public bool Constrain(ModuleSet allowed)
        {
            return false;
        }

        public void MarkClean()
        {
            isDirty = false;
        }

        public bool MarkDirty()
        {
            var result = !isDirty;
            isDirty = true;
            return result;
        }

        public ModuleSet GetAllowed(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}

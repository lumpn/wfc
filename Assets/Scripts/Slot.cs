using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    // wave function of modules
    public sealed class Slot
    {
        private readonly SlotType type;
        private ModuleSet modules;
        private bool isDirty = false;

        public Slot(SlotType type)
        {
            this.type = type;
            this.modules = type.allowed;
        }

        public bool Constrain(ModuleSet allowed)
        {
            return modules.Constrain(allowed);
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
            var result = new ModuleSet();
            foreach (int id in modules)
            {
                var module = type.modules[id];
                var allowed = module.GetAllowed(direction);
                result.Add(allowed);
            }
            return result;
        }
    }
}

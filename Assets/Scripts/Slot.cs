//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 

namespace Lumpn.WFC
{
    // wave function of modules
    public sealed class Slot
    {
        private readonly SlotType type;
        private BitSet modules;
        private bool isDirty = false;

        public Slot(SlotType type)
        {
            this.type = type;
            this.modules = type.allowed;
        }

        public bool Constrain(BitSet allowed)
        {
            return modules.IntersectWith(allowed);
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

        public BitSet GetAllowed(Direction direction)
        {
            var result = new BitSet();
            foreach (int id in modules)
            {
                var module = type.modules[id];
                var allowed = module.GetAllowed(direction);
                result.UnionWith(allowed);
            }
            return result;
        }
    }
}

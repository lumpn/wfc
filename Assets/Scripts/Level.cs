using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.WFC
{
    // collection of slots
    public class Level : MonoBehaviour
    {
        private Slot[,,] slots;


        public void Constrain(Vector3Int position, ModuleSet allowed)
        {
            var slot = GetSlot(position);
            var current = slot.modules;


            var changed = current.Constrain(allowed);
            Debug.Assert(current.ids != 0);

            if (changed)
            {
                // propagate
            }
        }

        private Slot GetSlot(Vector3Int position)
        {
            return slots[position.x, position.y, position.z];
        }
    }
}

using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleWitch.EquipmentStats.mm;

namespace LittleWitch
{
    class patch_EquipMenuEquipped : EquipMenuEquipped
    {
        private EquipSlot[] slots;

        private extern void orig_SetSelected(int n);
        private void SetSelected(int n)
        {
            if (n >= 0 && n < slots.Length)
            {
                StatState.SelectedSlot = slots[n];
            } else
            {
                StatState.SelectedSlot = null;
            }
            StatState.Dirty = true;

            orig_SetSelected(n);
        }
    }
}

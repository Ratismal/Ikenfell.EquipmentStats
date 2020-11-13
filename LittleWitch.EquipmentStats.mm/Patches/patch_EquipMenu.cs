using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleWitch.EquipmentStats.mm;

namespace LittleWitch
{
    class patch_EquipMenu : EquipMenu
    {
        private PartyMember selectedMember;
        private EquipmentType selectedItem;
        private EquipSlot selectedSlot;
        private EquipMenuStats statsMenu;

        public patch_EquipMenu(Action onComplete) : base(onComplete)
        {
        }

        public extern void orig_ctor(Action onComplete);
        [MonoModConstructor]
        public void ctor(Action onComplete)
        {
            orig_ctor(onComplete);

            this.OnLoad += delegate ()
            {
                Engine.OnUpdate += CheckToggle;
            };
            this.OnRemove += delegate ()
            {
                Engine.OnUpdate -= CheckToggle;
            };
        }

        public void CheckToggle()
        {
            if (Controls.Start.Pressed)
            {
                StatState.Toggled = !StatState.Toggled;
                StatState.Dirty = true;
            }

            if (StatState.Dirty && this.statsMenu != null && this.selectedMember != null) this.statsMenu.SetInfo(this.selectedMember, this.selectedItem);
        }
    }
}

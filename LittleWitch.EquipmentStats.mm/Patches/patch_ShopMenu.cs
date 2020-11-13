using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleWitch.EquipmentStats.mm;

namespace LittleWitch
{
    class patch_ShopMenu : ShopMenu
    {
        private PartyMember member;
        private ItemSlot item;
        private ShopMenuStats statsMenu;

        public patch_ShopMenu(Shop shop, Action onComplete) : base(shop, onComplete)
        {
        }

        public extern void orig_ctor(Shop shop, Action onComplete);
        [MonoModConstructor]
        public void ctor(Shop shop, Action onComplete)
        {
            orig_ctor(shop, onComplete);

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
                if (item.IsEquipment && this.statsMenu != null) this.statsMenu.SetInfo(this.member, this.item.Equipment);
            }
        }
    }
}

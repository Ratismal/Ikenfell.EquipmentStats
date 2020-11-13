using GameEngine;
using LittleWitch.EquipmentStats.mm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch
{
    class patch_ShopMenuStats : ShopMenuStats
    {
        private List<ShopMenuStats.Label> labels = new List<ShopMenuStats.Label>();
        private int updateDelay;
        private Action updateAction;

        public extern void orig_SetInfo(PartyMember member, EquipmentType item);
        public void SetInfo(PartyMember member, EquipmentType item)
        {
            this.updateDelay = 3;
            this.updateAction = delegate ()
            {
                foreach (ShopMenuStats.Label label in this.labels)
                {
                    bool flag = item == null || item.CanEquip(member);
                    if (item != null && StatState.Toggled)
                    {
                        var stat = item.GetStat(label.Stat).Val;
                        label.SetValue(stat, 0, item != null);
                    }
                    else if (flag)
                    {
                        int value;
                        int difference;
                        member.CalculateStatDifference(label.Stat, item, out value, out difference);
                        label.SetValue(value, difference, item != null);
                    }
                    else
                    {
                        label.SetValue(member.GetEquippedStatInField(label.Stat), 0, true);
                    }
                }
            };
        }


    }
}

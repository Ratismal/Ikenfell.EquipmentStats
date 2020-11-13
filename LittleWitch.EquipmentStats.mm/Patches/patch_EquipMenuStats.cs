using GameEngine;
using LittleWitch.EquipmentStats.mm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch
{
    class patch_EquipMenuStats : EquipMenuStats
    {
        private TextRenderer desc;
        private List<EquipMenuStats.Label> labels = new List<EquipMenuStats.Label>();

        public extern void orig_SetInfo(PartyMember member, EquipmentType item);
        public new void SetInfo(PartyMember member, EquipmentType item)
        {
            bool flag = item == null || string.IsNullOrEmpty(item.Description);
            EquipmentType equipped = null;
            if (StatState.SelectedSlot != null)
            {
                equipped = member.GetEquipment(StatState.SelectedSlot.Value);
            }
            if (StatState.Toggled && item == null && equipped != null && !string.IsNullOrEmpty(equipped.Description))
            {
                foreach (EquipMenuStats.Label label2 in this.labels)
                {
                    label2.SetVisible(false);
                }
                this.desc.Visible = true;
                this.desc.Text = equipped.Description;
            }
            else if (flag)
            {
                this.desc.Visible = false;
                foreach (EquipMenuStats.Label label in this.labels)
                {
                    int value;
                    int difference;
                    int stat = 0;
                    if (item != null)
                    {
                        stat = item.GetStat(label.Stat).Val;
                    } else if (StatState.Toggled && equipped != null)
                    {
                        stat = equipped.GetStat(label.Stat).Val;
                    }

                    member.CalculateStatDifference(label.Stat, item, out value, out difference);
                    label.SetVisible(true);
                    if (StatState.Toggled && (equipped != null || item != null))
                    {
                        label.SetValue(stat, 0, item != null);
                    } else
                    {
                        label.SetValue(value, difference, item != null);
                    }
                }
            }
            else
            {
                foreach (EquipMenuStats.Label label2 in this.labels)
                {
                    label2.SetVisible(false);
                }
                this.desc.Visible = true;
                this.desc.Text = item.Description;
            }
        }

    }
}

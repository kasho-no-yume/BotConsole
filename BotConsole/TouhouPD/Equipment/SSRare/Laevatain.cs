using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SSRare
{
    internal class Laevatain:Equip
    {
        public static new int sid = 3;
        public Laevatain()
        {
            name = "魔剑莱瓦汀";
            description = "剑身燃烧不止，散发比太阳还耀眼的光辉。持有者会继承魔剑的意志，斩杀破坏一切违背持有者" +
                "意志的事物。如此傲慢的意志，才能配的上魔剑之名吧。";
            quality = Quality.SSR;
            spellDescription = "每次使用普通攻击，都会额外给敌方1倍物理攻击的魔法伤害。";
            extraAtkRate = 20;
            extraAtk = 30;
            extraCriticalDmg = 15;
        }
        public override void Spell(WifeBase wife)
        {
            wife.OnAttackEvent += ExtraDamage;
        }
        private void ExtraDamage(WifeBase self,WifeBase? opponent)
        {
            if(opponent!=null)
            {
                opponent.BeingAttack(self, self.currentAttack, WifeBase.DamageType.magic);
            }
        }
    }
}

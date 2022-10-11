using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SSRare
{
    internal class Gungnir:Equip
    {
        public static new int sid = 2;
        public Gungnir()
        {
            name = "冈格尼尔";
            quality = Quality.SSR;
            description = "传说中奥丁所使用的武器，能够穿透一切。投掷时将划破星空。红魔馆馆主好像也有一把" +
                "一模一样的。";
            spellDescription = "嗜血成性的神枪，能够在攻击或者放技能的时候额外减少敌方50%自身攻击力的生命值，" +
                "并将生命值加到自己身上。";
            extraAtkRate = 20;
            extraAtk = 30;
            extraAtkPierceRate = 10;
            extraHpRate = 10;
        }
        private void Bleeding(WifeBase self,WifeBase? enemy)
        {
            if(enemy != null)
            {
                self.battleNotice.Add("冈格尼尔之枪的特质发动！");
                enemy.HpReduce(self.currentAttack / 2);
                self.HpGet(self.currentAttack / 2);
            }           
        }
        public override void Spell(WifeBase wife)
        {
            wife.OnAttackEvent += Bleeding;
            wife.OnSkillEvent += Bleeding;
        }
    }
}

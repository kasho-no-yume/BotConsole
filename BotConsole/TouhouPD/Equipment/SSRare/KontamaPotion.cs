using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SSRare
{
    internal class KontamaPotion:Equip
    {
        public static new int sid = 1;
        public KontamaPotion()
        {
            name = "绀珠之药";
            quality = Quality.SSR;
            description = "由天上药师八意永琳研发，能够让人不老不死的灵药。但是，这药的制作配方也许地上人永远" +
                "也学不会。此外，要是地上人知道了这药的存在，恐怕会引起世界大战吧。";
            spellDescription = "在死亡时，会满血复活，清除所有负面buff。";
            extraHpRate = 20;
            extraDefRate = 20;
            extraMdefRate = 20;
        }
        public override void Spell(WifeBase wife)
        {
            wife.OnDeathingEvent += Reincrnation;
        }
        private void Reincrnation(WifeBase self,WifeBase? none)
        {
            self.battleNotice.Add(name+"因绀珠之药的异能复活！");
            self.currentHp = self.maxHpFinal;
            self.OnDeathingEvent -= Reincrnation;
            for(int i=self.buffList.Count-1;i>=0;i--)
            {
                if (self.buffList[i].effect==Buff.BuffBase.Effect.negative)
                {
                    self.RemoveBuff(self.buffList[i]);
                }
            }
        }
    }
}

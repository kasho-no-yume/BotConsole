using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SSRare
{
    internal class UnderworldMirror:Equip
    {
        public static new int sid=0;
        public UnderworldMirror()
        {
            name = "异世之镜";
            quality = Quality.SSR;
            description = "能够向现世展示异世景色的镜子，其强大且怪异的异能完全无法用任何科学原理解释。也许只有" +
                "灵力强大到能够突破境界之力的人才能使用。";
            spellDescription = "装备者额外提高最大法力值10%的法术强度，每回合回复20%法力值。";
            extraMpRate = 15;
            extraMagic = 20;
            extraMagicPierceRate = 15;
            extraMdefRate = 20;
        }
        public override void Spell(WifeBase wife)
        {
            wife.currentMagic += wife.maxMpFinal / 10;
            wife.OnRoundStartEvent += HealMp;
        }
        private void HealMp(WifeBase self,WifeBase? nobody)
        {
            self.MpGet(self.maxMpFinal / 5);
        }
    }
}

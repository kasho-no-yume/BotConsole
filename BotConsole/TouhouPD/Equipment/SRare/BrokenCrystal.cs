using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class BrokenCrystal:Equip
    {
        public static new int sid = 102;
        public BrokenCrystal()
        {
            name = "破碎水晶";
            quality = Quality.SR;
            description = "不知因何而碎的水晶。但正因是碎水晶，其锋利度上了一个档次，拿这个划拉别人应该很痛吧。";
            extraAtkPierce = 10;
            extraAtkPierceRate = 5;
        }
    }
}

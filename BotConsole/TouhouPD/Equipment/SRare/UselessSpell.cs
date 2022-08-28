using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class UselessSpell:Equip
    {
        public static new int sid = 100;
        public UselessSpell()
        {
            name = "废弃的符卡";
            quality = Quality.SR;
            description = "丧失了曾经光辉的符卡。已经无法再发挥像样的灵力。但是装上这个，对于自身也是不小的提升。";
            extraMagic = 5;
            extraAtk = 5;
            extraDef = 5;
            extraMdef = 5;
        }
    }
}

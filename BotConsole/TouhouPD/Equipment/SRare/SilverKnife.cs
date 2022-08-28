using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class SilverKnife:Equip
    {
        public static new int sid = 107;
        public SilverKnife()
        {
            name = "银质飞刀";
            quality = Quality.SR;
            description = "使用银制作的飞刀，也许是专门为了猎杀吸血鬼而特制的。";
            extraAtkRate = 10;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class TurtleShell:Equip
    {
        public static new int sid = 105;
        public TurtleShell()
        {
            name = "神龟壳";
            quality = Quality.SR;
            description = "曾经是作为吉祥物，在香霖堂出售的存在。但是其本身似乎非常坚硬，应该可以抵御不少攻击。";
            extraDef = 8;
            extraMdef = 8;
        }
    }
}

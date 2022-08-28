using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class MadoBook:Equip
    {
        public static new int sid = 101;
        public MadoBook()
        {
            name = "魔导书";
            quality = Quality.SR;
            description = "也许是红魔馆地下大图书馆中的某本魔导书，光是拿着都能感觉到无穷无尽的魔力。";
            extraMagicRate = 10;
        }
    }
}

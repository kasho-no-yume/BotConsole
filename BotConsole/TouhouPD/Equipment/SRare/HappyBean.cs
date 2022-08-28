using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class HappyBean:Equip
    {
        public static new int sid = 104;
        public HappyBean()
        {
            name = "欢乐豆";
            quality = Quality.SR;
            description = "并不是用在牌桌上的欢乐豆，似乎是从什么地方抠出来的。";
            extraSpeed = 3;
            extraMagicPierce = 8;
            extraMagic = 8;
        }
    }
}

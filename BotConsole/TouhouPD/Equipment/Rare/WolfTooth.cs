using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class WolfTooth:Equip
    {
        public static new int sid = 1007;
        public WolfTooth()
        {
            name = "狼牙";
            quality = Quality.R;
            description = "嗜血成性的狼身上的牙，即便是带在身上也能感受到它主人曾拥有的无穷无尽的狂气。";
            extraCritical = 2;
        }
    }
}

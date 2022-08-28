using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class Sand:Equip
    {
        public static new int sid = 1009;
        public Sand()
        {
            name = "一把沙子";
            quality = Quality.R;
            description = "本身没什么攻击性，但是如果洒向敌方的话，可以让敌方打不到你。";
            extraMissrate = 1;
        }
    }
}

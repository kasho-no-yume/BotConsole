using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class LuckyRabbit:Equip
    {
        public static new int sid = 1004;
        public LuckyRabbit()
        {
            name = "幸运兔毛";
            quality = Quality.R;
            extraSpeed = 1;
            description = "被誉为幸运兔身上的毛发，也许不会给你带来幸运但可以让你逃的更快。";
        }
    }
}

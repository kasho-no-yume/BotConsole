using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class WoodenShield:Equip
    {
        public static new int sid = 1001;
        public WoodenShield()
        {
            name = "木盾";
            description = "装备虽破，但装上他，你似乎有金刚不坏之身。";
            extraDef = 2;
            quality = Quality.R;
        }
    }
}

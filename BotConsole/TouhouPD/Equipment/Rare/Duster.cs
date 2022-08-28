using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class Duster:Equip
    {
        public static new int sid = 1008;
        public Duster()
        {
            name = "指虎";
            quality = Quality.R;
            extraCriticalDmg = 3;
            description = "这玩意虽然不锋利，但是能帮你更好地把对面的牙齿打下来。";
        }
    }
}

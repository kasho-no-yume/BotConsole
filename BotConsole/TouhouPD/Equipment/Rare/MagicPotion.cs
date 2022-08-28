using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class MagicPotion:Equip
    {
        public static new int sid = 1006;
        public MagicPotion()
        {
            name = "魔法药";
            quality = Quality.R;
            extraMp = 5;
            description = "虽说是魔法药，但是和普通的要喝下去的魔法药不同，这个似乎带在身上就能有功效。";
        }
    }
    
}

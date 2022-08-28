using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class Yubi:Equip
    {
        public static new int sid = 1002;
        public Yubi()
        {
            quality = Quality.R;
            name = "御币";
            extraMagic = 1;
            description = "巫女祭神时会使用的御币，但是能发挥多少作用取决于巫女的凭依力，御币本身并无多大力量寄宿。";
        }
    }
}

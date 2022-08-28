using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class SpiritClothes:Equip
    {
        public static new int sid = 1003;
        public SpiritClothes()
        {
            quality = Quality.R;
            name = "灵衣";
            extraMdef = 2;
            description = "巫女祭神时所使用的衣服。也许能够帮助巫女祭神凭依时对抗邪祟侵蚀。";
        }
    }
}

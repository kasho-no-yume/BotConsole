using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class LeatherClothes:Equip
    {
        public static new int sid = 106;
        public LeatherClothes()
        {
            name = "皮衣";
            quality = Quality.SR;
            description = "使用鼠皮制作而成的衣服，不仅看起来更奢华了，而且也能当防弹衣穿。";
            extraHp = 100;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class MouseFur:Equip
    {
        public static new int sid = 1005;
        public MouseFur()
        {
            quality = Quality.R;
            name = "鼠皮";
            extraHp = 5;
            description = "无法考证究竟是什么动物的毛皮了，但是看起来很坚硬，能在挨打的时候少吃点苦。";
        }
    }
}

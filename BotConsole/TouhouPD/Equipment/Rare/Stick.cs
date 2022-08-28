using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.Rare
{
    internal class Stick:Equip
    {
        public static new int sid = 1000;
        public Stick()
        {
            name = "木棍";
            description = "也许在极大部分游戏里都会出现的，可能是用来凑数的装备。拿着这个，你可以追着面前的小孩子打" +
                "，装上这个，你就是名副其实的孩子王。";
            quality = Quality.R;
            extraAtk = 1;
        }
    }
}

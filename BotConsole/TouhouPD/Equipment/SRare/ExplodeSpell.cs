using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SRare
{
    internal class ExplodeSpell:Equip
    {
        public static new int sid = 103;
        public ExplodeSpell()
        {
            name = "起爆符";
            quality = Quality.SR;
            description = "隔壁片场来的，长相与普通的符无异。但是不会发出华丽的弹幕，而是会使空气急速膨胀，震飞" +
                "或灼伤敌人。";
            extraCritical = 4;
            extraCriticalDmg = 8;
        }
    }
}

using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment.SSRare
{
    internal class TenguFan : Equip
    {
        public static new int sid = 4;
        public WifeBase owner;
        public TenguFan()
        {
            name = "天狗的团扇";
            quality = Quality.SSR;
            description = "也许对天狗有这样的刻板印象，天狗总是会拿着团扇，好像团扇才是天狗速度快的原因。";
            spellDescription = "战斗开始时，自身的速度额外提高敌方速度的35%，若自身速度大于敌方，则根据自身速度与" +
                "敌方速度的比例按比提高闪避率。";
            extraSpeed = 10;
        }
        public override void Spell(WifeBase wife)
        {
            wife.OnRoundStartEvent += ExtraSpeed;
            owner = wife;
        }
        private void ExtraSpeed(WifeBase self,WifeBase? oppo)
        {
            if(oppo!=null)
            {
                self.currentSpeed += oppo.currentSpeed * 7 / 20;
                if(self.currentSpeed>oppo.currentSpeed)
                {
                    double rate = oppo.currentSpeed / (double)self.currentSpeed;
                    rate = 1 - rate;
                    var msrt =(int) (rate * 100);
                    self.currentMissrate += msrt;
                }
                owner.OnRoundStartEvent -= ExtraSpeed;
            }
        }
    }
}

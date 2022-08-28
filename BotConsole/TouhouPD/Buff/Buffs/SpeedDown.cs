using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class SpeedDown : BuffBase
    {
        public SpeedDown(int sustainRound,int strength) : base(sustainRound,strength)
        {
            name = "速度降低";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            wife.currentSpeed -= strength;
            base.BeingAdded(wife);
        }
        public override void BeingRemoved(WifeBase wife)
        {
            wife.currentSpeed += strength;
            base.BeingRemoved(wife);
        }
        public override void Repeat(BuffBase buff)
        {
            strength += buff.strength;
            owner.currentSpeed -= buff.strength;
            base.Repeat(buff);
        }
    }
}

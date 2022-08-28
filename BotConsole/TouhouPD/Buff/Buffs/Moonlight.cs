using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Moonlight : BuffBase
    {
        public Moonlight(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "月亮光";
            effect = Effect.positive;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentMissrate += strength;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentMissrate -= strength;
        }
    }
}

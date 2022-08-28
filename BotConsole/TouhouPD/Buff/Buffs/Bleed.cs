using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Bleed : BuffBase
    {
        public Bleed(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "流血";
            effect = Effect.negative;
        }
        public override void LastingEffect(WifeBase wife)
        {
            base.LastingEffect(wife);
            wife.HpReduce(strength);
        }

    }
}

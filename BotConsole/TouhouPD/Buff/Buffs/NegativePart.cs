using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class NegativePart : BuffBase
    {
        public NegativePart(int sustainRound) : base(sustainRound)
        {
            name = "反粒子";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentAttack = (int)(wife.currentAttack*0.7);
            wife.currentMagic = (int)(wife.currentMagic * 0.7);
            wife.currentSpeed = (int)(wife.currentSpeed * 0.7);
            wife.currentDefend = (int)(wife.currentDefend * 0.7);
            wife.currentMdefend = (int)(wife.currentMdefend * 0.7);           
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentAttack = (int)(wife.currentAttack / 0.7);
            wife.currentMagic = (int)(wife.currentMagic / 0.7);
            wife.currentSpeed = (int)(wife.currentSpeed / 0.7);
            wife.currentDefend = (int)(wife.currentDefend / 0.7);
            wife.currentMdefend = (int)(wife.currentMdefend / 0.7);
        }
    }
}

using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Scale : BuffBase
    {
        public Scale(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "鳞粉";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentAttack -= strength;
            wife.currentMagic -= strength;
            wife.currentMdefend -= strength;
            wife.currentDefend -= strength;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentAttack += strength;
            wife.currentMagic += strength;
            wife.currentMdefend += strength;
            wife.currentDefend += strength;
        }
        public override void Repeat(BuffBase buff)
        {
            strength += buff.strength;
            owner.currentAttack -= buff.strength;
            owner.currentMagic -= buff.strength;
            owner.currentMdefend -= buff.strength;
            owner.currentDefend -= buff.strength;
        }
    }
}

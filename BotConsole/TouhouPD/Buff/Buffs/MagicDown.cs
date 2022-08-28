using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class MagicDown:BuffBase
    {
        public MagicDown(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "法术降低";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentMagic -= strength;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentMagic += strength;
        }
        public override void Repeat(BuffBase buff)
        {
            owner.currentMagic -= buff.strength;
            strength += buff.strength;
        }
    }
}

using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class AttackDown : BuffBase
    {
        public AttackDown(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "攻击力降低";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentAttack -= strength;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentAttack += strength;
        }
        public override void Repeat(BuffBase buff)
        {
            owner.currentAttack -= buff.strength;
            strength += buff.strength;
        }
    }
}

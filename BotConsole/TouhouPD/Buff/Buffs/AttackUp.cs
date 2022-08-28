using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class AttackUp : BuffBase
    {
        public AttackUp(int sustainRound, int strength) : base(sustainRound, strength)
        {
            name = "攻击力提升";
            effect = Effect.positive;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.currentAttack += strength;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.currentAttack -= strength;
        }

    }
}

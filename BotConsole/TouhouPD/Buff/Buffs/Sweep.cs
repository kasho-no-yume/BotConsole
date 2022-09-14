using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Sweep : BuffBase
    {
        public Sweep(int sustainRound,int strength) : base(sustainRound,strength)
        {
            name = "扫除";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.OnBuffAdded += GoDamage;
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.OnBuffAdded -= GoDamage;
        }
        private void GoDamage(BuffBase buff)
        {
            owner.BeingAttack(owner,strength,WifeBase.DamageType.physical);
        }
    }
}

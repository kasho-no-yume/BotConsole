using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class PositivePart : BuffBase
    {
        public PositivePart(int sustainRound) : base(sustainRound)
        {
            name = "正粒子";
            effect = Effect.negative;
        }
        public override void BeingAdded(WifeBase wife)
        {
            base.BeingAdded(wife);
            wife.OnBuffAdded+=BuffInvalid;
            for(int i=wife.buffList.Count-1;i>=0;i--)
            {
                if(wife.buffList[i].effect==Effect.positive)
                {
                    wife.RemoveBuff(wife.buffList[i]);
                }
            }
        }
        public override void BeingRemoved(WifeBase wife)
        {
            base.BeingRemoved(wife);
            wife.OnBuffRemoved -= BuffInvalid;
        }
        private void BuffInvalid(BuffBase buff)
        {
            if(buff.effect==Effect.positive)
            {
                buff.sustainRound=0;
            }
        }
    }
}

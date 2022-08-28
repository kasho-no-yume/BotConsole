using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Orleans : BuffBase
    {
        public Orleans(int sustainRound) : base(sustainRound)
        {
            effect = Effect.positive;
            name = "奥尔良人偶";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff.Buffs
{
    internal class Shanghai : BuffBase
    {
        public Shanghai(int sustainRound) : base(sustainRound)
        {
            name = "上海人偶";
            effect = Effect.positive;
        }
    }
}

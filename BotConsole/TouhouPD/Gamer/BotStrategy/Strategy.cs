using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{  
    internal abstract class Strategy
    {
        protected WifeBase wife;
        protected WifeBase? enemy;
        public Strategy(WifeBase wife)
        {
            this.wife = wife;
        }
        protected Strategy(WifeBase wife, WifeBase? enemy) : this(wife)
        {
            this.enemy = enemy;
        }

        public abstract string HowToDo();

    }
}

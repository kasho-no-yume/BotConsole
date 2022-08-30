using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD.Wife.Wives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{
    internal class YorihimeStra : Strategy
    {
        public Queue<int> skillSeq;
        public YorihimeStra(WifeBase wife) : base(wife)
        {
            skillSeq = new Queue<int>();
            this.wife = wife as Yorihime;
            skillSeq.Enqueue(2);
            skillSeq.Enqueue(2);
            skillSeq.Enqueue(3);
            skillSeq.Enqueue(3);           
        }

        public override string HowToDo()
        {
            if(wife.CanUseSkill(skillSeq.Peek()))
            {
                skillSeq.Enqueue(skillSeq.Peek());
                return "skill" + skillSeq.Dequeue();
            }
            return "attack";
        }
    }
}

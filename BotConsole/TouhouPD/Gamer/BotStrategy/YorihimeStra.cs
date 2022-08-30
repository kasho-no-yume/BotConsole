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
        public Yorihime yorihime;
        public YorihimeStra(WifeBase wife) : base(wife)
        {
            skillSeq = new Queue<int>();
            yorihime = (Yorihime)wife;
            skillSeq.Enqueue(2);
            skillSeq.Enqueue(2);
            skillSeq.Enqueue(3);
            skillSeq.Enqueue(3);           
        }

        public override string HowToDo()
        {
            if(!yorihime.gionsama&&yorihime.filthyGod&&yorihime.CanUseTwo())
            {
                return "skill2";
            }
            if(yorihime.amaterasu<=0&&!yorihime.filthyGod && yorihime.CanUseTwo())
            {
                return "skill2";
            }
            if(yorihime.amatsumi<=0&&yorihime.filthyGod && yorihime.CanUseThree())
            {
                return "skill3";
            }
            if(yorihime.kanayamahi <= 0 && !yorihime.filthyGod && yorihime.CanUseThree())
            {
                return "skill3";
            }
            if(yorihime.gionsama&&yorihime.filthyGod&&yorihime.CanUseOne())
            {
                return "skill1";
            }
            return "attack";
        }
    }
}

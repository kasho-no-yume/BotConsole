using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{
    internal class CirnoStra : Strategy
    {
        private Queue<int> skills;
        public CirnoStra(WifeBase wife) : base(wife)
        {
            skills = new Queue<int>();
            skills.Enqueue(1);
            skills.Enqueue(3);
        }

        public override string HowToDo()
        {
            if(wife.CanUseSkill(skills.Peek()))
            {
                var sk = skills.Dequeue();
                skills.Enqueue(sk);
                return "skill"+sk;
            }
            if(wife.CanUseSkill(0))
            {
                return "attack";
            }
            return "defend";
        }
    }
}

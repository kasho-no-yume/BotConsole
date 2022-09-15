using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD.Wife.Wives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{
    internal class MarisaStra : Strategy
    {
        public Marisa self;
        private Queue<int> seq;
        public MarisaStra(WifeBase wife, WifeBase? enemy) : base(wife, enemy)
        {
            self = (Marisa)wife;
            seq = new Queue<int>();
            seq.Enqueue(1);
            seq.Enqueue(2);
            seq.Enqueue(2);
        }

        public override string HowToDo()
        {
            if(CanDefeat()&&self.CanUseSkill(3))
            {
                return "skill3";
            }
            if(self.CanUseSkill(seq.Peek()))
            {
                var res = seq.Peek();
                seq.Enqueue(seq.Dequeue());
                return "skill" + res;
            }
            if(wife.CanUseSkill(0))
            {
                return "attack";
            }
            return "defend";
        }
        private bool CanDefeat()
        {
            var extra = self.currentMp / 2;
            int damage = (self.currentMagic + extra) * 2;
            damage *= (10 + self.rounds) / 10;
            int mdef = enemy.currentMdefend;
            damage = damage * 100 / (100 + mdef);
            if(damage>=enemy.currentHp)
            {
                return true;
            }
            return false;
        }
    }
}

using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD.Wife.Wives.ScarletDevil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{
    internal class RemiliaStra : Strategy
    {
        private int skillnum = 1;
        private string[] skillChoose = new string[4];
        private int[] skillSeq=new int[4];
        public RemiliaStra(WifeBase wife) : base(wife)
        {
            this.wife = wife as Remilia;
            skillChoose[1] = "skill2";
            skillChoose[2] = "skill1";
            skillChoose[3] = "skill3";
            skillSeq[1] = 2;
            skillSeq[2] = 1;
            skillSeq[3] = 3;
        }
        
        public override string HowToDo()
        {
            if(skillnum>3)
            {
                skillnum = 1;
            }
            if (wife.CanUseSkill(skillSeq[skillnum]))
            {
                return skillChoose[skillnum++];
            }
            if(wife.CanUseSkill(0))
            {
                return "attack";
            }
            return "defend";
        }
    }
}

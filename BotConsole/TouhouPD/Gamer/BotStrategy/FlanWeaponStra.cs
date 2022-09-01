using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD.Wife.Wives.ScarletDevil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy
{
    internal class FlanWeaponStra : Strategy
    {
        public int cool;

        public FlanWeaponStra(WifeBase wife) : base(wife)
        {
            cool = 0;
            this.wife = wife as Flandre;
        }

        public override string HowToDo()
        {
            if(wife.CanUseTwo())
            {
                return "skill2";                
            }
            if(wife.CanUseSkill(0))
            {
                return "attack";
            }
            return "defend";
        }
    }
}

using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Gamer.BotStrategy;
using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer
{
    internal class GameBot : Participant
    {
        private Strategy? strategy=null;
        public GameBot(string name,WifeBase wife,Equip? equip,Strategy? s)
        {
            this.weapon = equip;
            this.name = name;
            this.wife = wife;
            this.user = null;
            strategy = s;
            iden = "bot";
        }
        public override string RequireAct()
        {
            if(strategy==null)
            {
                if(wife.CanUseSkill(0))
                {
                    return "attack";
                }
                return "defend";
            }
            return strategy.HowToDo();
        }
    }
}

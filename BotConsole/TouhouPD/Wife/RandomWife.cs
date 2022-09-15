using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotConsole.TouhouPD.Wife.Wives;
using BotConsole.TouhouPD.Wife.Wives.ScarletDevil;
using BotConsole.TouhouPD.Wife.Wives.HiddenStar;
using BotConsole.TouhouPD.Wife.Wives.CherryBlossom;

namespace BotConsole.TouhouPD.Wife
{
    internal static class RandomWife
    {
        private static Dictionary<int, int> wifeWeight = new Dictionary<int, int>();
        private static int weightSum = 0;
        static RandomWife()
        {
            wifeWeight.Add(Merry.sid, Merry.weight);
            wifeWeight.Add(Marisa.sid, Marisa.weight);
            wifeWeight.Add(Rumia.sid, Rumia.weight);
            wifeWeight.Add(Cirno.sid, Cirno.weight);
            wifeWeight.Add(Daiyousei.sid, Daiyousei.weight);
            wifeWeight.Add(MeiLing.sid, MeiLing.weight);
            wifeWeight.Add(Koakuma.sid, Koakuma.weight);
            wifeWeight.Add(Patchouli.sid, Patchouli.weight);
            wifeWeight.Add(Sakuya.sid, Sakuya.weight); 
            wifeWeight.Add(Remilia.sid, Remilia.weight);
            wifeWeight.Add(Flandre.sid, Flandre.weight);
            wifeWeight.Add(Alice.sid, Alice.weight);
            wifeWeight.Add(Yorihime.sid, Yorihime.weight);
            wifeWeight.Add(EternityLarva.sid, EternityLarva.weight);
            wifeWeight.Add(Sakata.sid, Sakata.weight);
            wifeWeight.Add(KomanoAunn.sid, KomanoAunn.weight);
            foreach(var i in wifeWeight)
            {
                weightSum += i.Value;
            }
        }
        public static int RandomId()
        {
            int randomWeight = new Random().Next(1, weightSum);
            foreach(var i in wifeWeight)
            {
                if(randomWeight<=i.Value)
                {
                    return i.Key;
                }
                randomWeight-=i.Value;
            }
            return -1;
        }
    }
}

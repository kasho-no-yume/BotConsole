using BotConsole.TouhouPD.Wife.Wives;
using BotConsole.TouhouPD.Wife.Wives.ScarletDevil;
using BotConsole.TouhouPD.Wife.Wives.HiddenStar;
using BotConsole.TouhouPD.Wife.Wives.CherryBlossom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife
{
    internal class WifeFactory
    {
        public static Dictionary<int, Type> id2Name;
        static WifeFactory()
        {
            id2Name=new Dictionary<int, Type>();
            id2Name.Add(0, typeof(Merry));
            id2Name.Add(Yorihime.sid, typeof(Yorihime));
            id2Name.Add(Rumia.sid, typeof(Rumia));
            id2Name.Add(Cirno.sid,typeof(Cirno));
            id2Name.Add(Daiyousei.sid, typeof(Daiyousei));
            id2Name.Add(MeiLing.sid,typeof(MeiLing));
            id2Name.Add(Koakuma.sid,typeof(Koakuma));
            id2Name.Add(Patchouli.sid,typeof(Patchouli));
            id2Name.Add(Sakuya.sid,typeof(Sakuya));
            id2Name.Add(Remilia.sid,typeof(Remilia));
            id2Name.Add(Flandre.sid,typeof(Flandre));
            id2Name.Add(Alice.sid, typeof(Alice));
            id2Name.Add(EternityLarva.sid,typeof(EternityLarva));
            id2Name.Add(Sakata.sid,typeof(Sakata));
            
        }
        /// <summary>
        /// 确保不要传不存在的id进来
        /// </summary>
        /// <param name="id"></param>
        /// <param name="level"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static WifeBase GenerateWife(int id, int level,int exp)
        {
            var wife = GenerateWife(id, level);
            wife.GetExp(exp);
            return wife;
        }
        public static WifeBase? GenerateWife(int id,int level)
        {
            WifeBase? wife=null;
            if(id2Name.ContainsKey(id))
            {
                wife = (WifeBase?)System.Activator.CreateInstance(id2Name[id]);
                wife.SetLevel(level);
            }
            return wife;
        }
    }
}

using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer
{
    internal abstract class Participant
    {
        public WifeBase wife;
        public Equip? weapon=null;
        public User? user=null;
        public string name;
        /// <summary>
        /// 前标识后值，支持money，exp，equip
        /// </summary>
        public List<KeyValuePair<string,int>> bouns;
        public Participant(WifeBase wife)
        {
            this.wife = wife;
            bouns = new List<KeyValuePair<string, int>>();
        }
        public Participant()
        {
            bouns = new List<KeyValuePair<string, int>>();
        }
        /// <summary>
        /// 当轮到该参与者行动时，请求该参与者做出一个行动响应。
        /// </summary>
        /// <returns>返回监听到的请求结果
        /// 可能有，攻击，技能123，防御，状态，技能，认输，超时
        /// </returns>
        public abstract string RequireAct();
    }
}

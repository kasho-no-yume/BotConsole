using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class BattleNotice
    {
        private string group;
        private List<ForwardMsg> noticeList;
        public BattleNotice(string group)
        {
            this.group = group;
            noticeList = new List<ForwardMsg>();
        }
        public void Add(string notice)
        {
            noticeList.Add(new ForwardMsg("鸡气人", "2868534536", notice));
        }
        public void SendNotice()
        {
            if(noticeList.Count > 0)
            {
                new Sender().QuicklySendForward(group, noticeList);
            }
            noticeList.Clear();
        }
    }
}

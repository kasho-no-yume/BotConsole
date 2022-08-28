using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.Util
{
    internal class ForwardMsg
    {
        public class Data
        {
            public string name { get; set; }
            public string uin { get; set; }
            public string content { get; set; }
        }
        public string type { get; set; }
        public Data data { get; set; }
        public ForwardMsg(string name,string uin,string content)
        {
            data = new Data();
            type = "node";
            data.name=name; 
            data.uin=uin;
            data.content=content;
        }

    }
}

using BotConsole.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class Repeat
    {
        public static void RecvRepeat(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string msg = json["message"].ToString();
                if(msg.Split(" ")[0].Equals("复读")&&msg.Split(" ").Length>1)
                {
                    JObject res = new JObject();
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("group_id", json["group_id"].ToString());
                    string content = msg.Substring(3);
                    string cq = CQUtil.QuickGetCQ("tts", "text="+content);
                    message.Add("message", cq);
                    res = JsonUtil.SerialFromDic(message) ;
                    Console.WriteLine(res.ToString());
                    new Sender().Send("send_group_msg", res);
                }
            }
        }
    }
}

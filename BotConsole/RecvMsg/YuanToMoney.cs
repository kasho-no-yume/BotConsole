using BotConsole.TouhouPD;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class YuanToMoney
    {
        public static void Transfer(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string[] msg = json["message"].ToString().Split(" ");
                if(msg.Length>1)
                {
                    if (msg[0].Equals("兑换马灵"))
                    {
                        if (int.TryParse(msg[1], out int amount))
                        {
                            string qq = json["sender"]["user_id"].ToString();
                            string group = json["group_id"].ToString();
                            var user = new User(qq, group);
                            if(amount<=0)
                            {
                                new Sender().QuicklyReply(user.group,"请输入大于0的正数");
                                return;
                            }
                            if(user.CostMoney(amount))
                            {
                                string sendmsg = "{\\\"money\\\":"+ amount + ",\\\"id\\\":"+ qq
                                    + ",\\\"group\\\":"+ group + ",\\\"rate\\\":1000}";
                                var pack=new Dictionary<string,string>();
                                pack.Add("message_type", "private");
                                pack.Add("user_id", "2090037424");
                                pack.Add("message", sendmsg);
                                new Sender().Send("send_msg", JsonUtil.SerialFromDic(pack));
                            }
                        }
                    }
                }              
            }
        }
    }
}

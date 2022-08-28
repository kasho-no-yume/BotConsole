using BotConsole.TouhouPD;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class RecvTransfer
    {
        public static void Transfer(JObject json)
        {
            if (json["message_type"].ToString().Equals("private"))
            {
                if (json["user_id"].ToString().Equals("2090037424"))
                {
                    string recvmsg = json["message"].ToString();
                    var j=JObject.Parse(recvmsg);
                    if(j.ContainsKey("rate"))
                    {
                        int finalMoney = (int)j["money"] * 1000 / (int)j["rate"];
                        if (finalMoney<=0)
                        {
                            return;
                        }
                        var user = new User(j["id"].ToString(), j["group"].ToString());
                        user.GetMoney(finalMoney);
                        var pack = new Dictionary<string, string>();
                        pack.Add("message_type", "private");
                        pack.Add("user_id", "2090037424");
                        pack.Add("message", "{\\\"id\\\":"+user.qq+",\\\"msg\\\":\\\"成功\\\"}");
                        new Sender().Send("send_msg", JsonUtil.SerialFromDic(pack));
                        new Sender().QuicklyReply(user.group,"成功兑换"+finalMoney+"円。");
                    }                   
                }
            }
        }
    }
}

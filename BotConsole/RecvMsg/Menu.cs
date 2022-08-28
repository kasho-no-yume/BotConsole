using BotConsole.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class Menu
    {
        static string menu = "鸡气鸡菜单，你可以输入以下指令：\n" +
            "【复读 内容】，以甜美的汉子音复读内容（被弃用）\n" +
            "【带\\\"妹子\\\"或\\\"少女\\\"的句子】，发一张二次元美少女的图\n" +
            "【查看gal黄油】，列出当前的所有gal黄油\n" +
            "【查看非gal黄油】，列出当前所有的非gal黄油。\n" +
            "【养老婆】  可以查看群大型养老婆对战游戏的指令规则\n" +
            "【兑换马灵 钱数】消耗钱数，兑换成群主巨佬机器人的货币马灵。";
        public static void ShowMenu(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string msg = json["message"].ToString();
                if (msg.Equals("菜单") || msg.Equals("帮助"))
                {
                    Dictionary<string,string> rmsg=new Dictionary<string,string>();
                    rmsg.Add("group_id", json["group_id"].ToString());
                    rmsg.Add("message", menu);
                    var rjson = JsonUtil.SerialFromDic(rmsg);
                    new Sender().Send("send_group_msg", rjson);
                }
            }
        }
    }
}

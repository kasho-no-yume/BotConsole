using BotConsole.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class RandomPic
    {
        public static void RandomAcg(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string msg = json["message"].ToString();
                if(msg.Contains("少女")||msg.Contains("妹子"))
                {
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.ixiaowai.cn/api/api.php");
                        req.Method = "HEAD";
                        req.AllowAutoRedirect = true;
                        WebResponse response = req.GetResponse();
                        string newurl= response.ResponseUri.ToString();
                        Console.WriteLine(newurl);
                        Dictionary<string, string> map = new Dictionary<string, string>();
                        Dictionary<string, string> pic = new Dictionary<string, string>();
                        pic.Add("file", newurl);
                        pic.Add("url", newurl);
                        map.Add("group_id", json["group_id"].ToString());
                        map.Add("message", CQUtil.GetCQString("image", pic));
                        JObject rjson = JsonUtil.SerialFromDic(map);
                        new Sender().SendAsync("send_group_msg", rjson);
                        Console.WriteLine(rjson.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
    }
}

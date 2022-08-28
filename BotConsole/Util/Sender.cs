using BotConsole.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole
{
    internal class Sender
    {
        private string boturl = "http://127.0.0.1:5700/";
        public JObject Send(string baseRoute, JObject json )
        {
            return RequestJson(boturl, baseRoute, json);
        }
        public JObject SendAsync(string baseRoute,JObject json)
        {
            return RequestJsonAsync(boturl, baseRoute, json).Result;
        }
        public JObject QuicklyReply(string groupid,string msg)
        {
            Dictionary<string, string> rmsg = new Dictionary<string, string>();
            rmsg.Add("group_id", groupid);
            rmsg.Add("message", msg);
            var rjson = JsonUtil.SerialFromDic(rmsg);
            return new Sender().Send("send_group_msg", rjson);
        }
        public JObject QuicklyReplyAsync(string groupid,string msg)
        {
            Dictionary<string, string> rmsg = new Dictionary<string, string>();
            rmsg.Add("group_id", groupid);
            rmsg.Add("message", msg);
            var rjson = JsonUtil.SerialFromDic(rmsg);
            Console.WriteLine(rjson.ToString());
            var res = SendAsync("send_group_msg", rjson);
            Console.WriteLine(res.ToString());
            return res;
        }
        public JObject RequestJson(string url,string baseRoute,JObject json)
        {
            string finalUrl = url + baseRoute;
            var req = HttpWebRequest.Create(finalUrl);
            req.Method = "POST";
            req.ContentType = "application/json";
            var dataStream = new StreamWriter(req.GetRequestStream());
            dataStream.Write(json.ToString());
            dataStream.Flush();
            dataStream.Close();
            var res = req.GetResponse();
            var stream = res.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var jstr = reader.ReadToEnd();
            JObject rjson = JObject.Parse(jstr);
            res.Close();
            return rjson;
        }
        public JObject RequestGet(string url)
        {
            HttpClient webRequest = new HttpClient();
            var res=webRequest.GetAsync(url).Result;
            string resStr = res.Content.ReadAsStringAsync().Result;
            return JObject.Parse(resStr);
        }
        public async Task<JObject> RequestJsonAsync(string url, string baseRoute, JObject json)
        {
            string finalUrl = url + baseRoute;
            var client = new HttpClient();
            var res = await client.PostAsync(finalUrl, new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
            var str = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(str);
            return JObject.Parse(str);
        }
        public JObject QuicklySendForward(string group,List<ForwardMsg> msg)
        {
            JArray jArray = new JArray();
            foreach (var i in msg)
            {
                JObject jobj = JObject.Parse(JsonConvert.SerializeObject(i));                
                jArray.Add(jobj);
            }
            JObject send = new JObject();
            send.Add("group_id", group);
            send.Add("messages", jArray);
            return SendAsync("send_group_forward_msg",send);
        }
        public JObject QuicklySendForward(string group, ForwardMsg msg)
        {
            JArray jArray = new JArray();
            JObject jobj = JObject.Parse(JsonConvert.SerializeObject(msg));
            jArray.Add(jobj);
            JObject send = new JObject();
            send.Add("group_id", group);
            send.Add("messages", jArray);
            return SendAsync("send_group_forward_msg", send);
        }
    }
}

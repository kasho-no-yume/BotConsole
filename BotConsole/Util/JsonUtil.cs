using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole
{
    internal class JsonUtil
    {
        public static JObject SerialFromDic(Dictionary<string,string> dic)
        {
            string jsonstr = "{";
            foreach(var i in dic)
            {
                jsonstr += string.Format(",\"{0}\":\"{1}\"", i.Key, i.Value);
            }
            jsonstr=jsonstr.Remove(1,1);
            jsonstr += '}';
            return JObject.Parse(jsonstr);
        }
    }
}

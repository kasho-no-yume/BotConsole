using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole
{
    internal class CQUtil
    {
        public static string GetCQString(string type, Dictionary<string, string> dic)
        {
            string para = "";
            foreach(var i in dic)
            {
                para += (','+i.Key+'='+i.Value);
            }
            para=para.Replace("&", "&amp;");
            para=para.Replace("[", "&#91;");
            para=para.Replace("]", "&#93;");
            para=para.Replace(",", "&#44;");
            //para.Remove(para.Length-1,1);
            string res = string.Format("[CQ:{0}{1}]",type,para);
            return res;
        }
        public static string QuickGetCQ(string cq,string para)
        {
            para=para.Replace("&", "&amp;");
            para=para.Replace("[", "&#91;");
            para=para.Replace("]", "&#93;");
            para=para.Replace(",", "&#44;");
            return string.Format("[CQ:{0},{1}]",cq,para);
        }
        /// <summary>
        /// 确保传入的是正确的cq码！
        /// </summary>
        /// <param name="cq"></param>
        /// <returns></returns>
        public static Dictionary<string,string> AnalysisCQ(string cq)
        {
            Dictionary<string,string> result = new Dictionary<string,string>();
            cq = cq.Remove(cq.Length - 1);
            string[] param = cq.Split(",");            
            for(int i=1;i<param.Length;i++)
            {
                param[i] = param[i].Replace("&amp;", "&");
                param[i] = param[i].Replace("&#91;", "[");
                param[i] = param[i].Replace("&#93;", "]");
                param[i] = param[i].Replace("&#44;", ",");
                string type = param[i].Split("=")[0];
                string content = param[i].Split("=")[1];
                result.Add(type, content);
            }
            return result;
        }
        public static bool IsCQCode(string cq)
        {
            bool res = false;
            if(cq.StartsWith("[CQ:")&&cq.EndsWith(']'))
            {
                return true;
            }
            return res;
        }
    }
}

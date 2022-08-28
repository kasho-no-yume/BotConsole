using BotConsole.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class GalEroge
    {
        public static void ShowGals(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string msg = json["message"].ToString();
                if (msg.Split(" ")[0].Equals("查看gal黄油"))
                {
                    if(msg.Split(" ").Length==1)
                    {
                        var con = new DBMgr("erogemanager","a1935515130","eroges");
                        var reader = con.Search("select id,anoname from gals");
                        string gallist = "";
                        while(reader.Read())
                        {
                            gallist += reader["id"].ToString()+'.'+
                                reader["anoname"].ToString() + '\n';
                        }
                        string finalmsg = "现在有如下gal（输入\\\"查看gal黄油 序号\\\"来查看详情）:\n" +
                            "（输入\\\"查看gal黄油 部分名字\\\"来搜索gal）"
                            + gallist;
                        reader.Close();
                        Dictionary<string, string> sendmsg = new Dictionary<string, string>();
                        sendmsg.Add("group_id", json["group_id"].ToString());
                        sendmsg.Add("message", finalmsg);
                        JObject rjson=JsonUtil.SerialFromDic(sendmsg);
                        new Sender().Send("send_group_msg", rjson);
                    }
                    else if(msg.Split(" ").Length == 2)
                    {
                        try
                        {
                            bool isId = int.TryParse(msg.Split(" ")[1], out int num);
                            var con = new DBMgr("erogemanager", "a1935515130", "eroges");
                            if (isId)
                            {
                                var reader = con.Search("select * from gals where id=" + num);
                                if (reader.Read())
                                {
                                    string galinfo = "";
                                    galinfo += "gal原名：" + reader["oriname"].ToString() + '\n';
                                    galinfo += "gal别名：" + reader["anoname"].ToString() + '\n';
                                    galinfo += "剧情性评级：" + reader["scriptrank"].ToString() + '\n';
                                    galinfo += "社保度评级：" + reader["erorank"].ToString() + '\n';                                   
                                    galinfo += "黄油介绍：" + reader["description"].ToString() + '\n';
                                    Dictionary<string, string> sendmsg = new Dictionary<string, string>();
                                    sendmsg.Add("group_id", json["group_id"].ToString());
                                    sendmsg.Add("message", galinfo);
                                    JObject rjson = JsonUtil.SerialFromDic(sendmsg);
                                    new Sender().Send("send_group_msg", rjson);
                                }
                                else
                                {
                                    Dictionary<string, string> sendmsg = new Dictionary<string, string>();
                                    sendmsg.Add("group_id", json["group_id"].ToString());
                                    sendmsg.Add("message", "序号输入有误，是不是太大了呢？");
                                    JObject rjson = JsonUtil.SerialFromDic(sendmsg);
                                    new Sender().Send("send_group_msg", rjson);
                                }
                                reader.Close();
                            }
                            else if (!isId)
                            {
                                var reader = con.Search("select * from gals where anoname like '%" + msg.Split(" ")[1] + "%'");
                                if (reader.HasRows)
                                {
                                    string gallist = "";
                                    while (reader.Read())
                                    {
                                        gallist += reader["id"].ToString() + '.' +
                                            reader["anoname"].ToString() + '\n';
                                    }
                                    string finalmsg = gallist;
                                    reader.Close();
                                    Dictionary<string, string> sendmsg = new Dictionary<string, string>();
                                    sendmsg.Add("group_id", json["group_id"].ToString());
                                    sendmsg.Add("message", finalmsg);
                                    JObject rjson = JsonUtil.SerialFromDic(sendmsg);
                                    new Sender().Send("send_group_msg", rjson);
                                }
                                else
                                {
                                    Dictionary<string, string> sendmsg = new Dictionary<string, string>();
                                    sendmsg.Add("group_id", json["group_id"].ToString());
                                    sendmsg.Add("message", "搜索有误，是不是打错字了呢？");
                                    JObject rjson = JsonUtil.SerialFromDic(sendmsg);
                                    new Sender().Send("send_group_msg", rjson);
                                }
                                reader.Close();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
        }
    }
}

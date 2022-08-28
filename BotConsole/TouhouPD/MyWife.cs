using BotConsole.TouhouPD.Wife;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class MyWife
    {
        public static void CheckWives(User user)
        {
            string qq = user.qq;
            string res = "";
            string sqlcmd = "select * from ownedwife where qq='"+qq+"'";
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sqlcmd);
            int current = user.GetConfront();
            if(reader.HasRows)
            {
                
                while (reader.Read())
                {
                    var wife = WifeFactory.GenerateWife((int)reader["id"], (int)reader["level"]);
                    if(current==wife.id)
                    {
                        res += '*';
                    }
                    res+=wife.id+" "+wife.name+" lv."+wife.level+"\n";
                }
                res += "输入【查看老婆 序号】查看老婆的详细信息\n";
                res += "输入【老婆出战 序号】将目标老婆设为出战";
                new Sender().QuicklyReply(user.group, res);
            }
            else
            {
                new Sender().QuicklyReply(user.group,"你当前还没有老婆哦。\n输入【供奉】可以花1000円抽老婆哦。");
            }
        }
        public static void WifeDetail(User user,string param)
        {
            string[] p = param.Split(" ");
            if(p.Length>1)
            {
                int id;
                if (int.TryParse(p[1],out id))
                {
                    user.ShowWifeDetail(id);
                }
            }
        }
        public static void SetConfront(User user,string param)
        {
            string[] p = param.Split(" ");
            if (p.Length > 1)
            {
                int id;
                if (int.TryParse(p[1], out id))
                {
                    user.SetConfront(id);
                }
            }
        }
    }
}

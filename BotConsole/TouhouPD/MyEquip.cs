using BotConsole.TouhouPD.Equipment;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class MyEquip
    {

        public static void ShowEquipList(User user)
        {
            string cmd = string.Format("select * from equipdata where qq='{0}'",user.qq);
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(cmd);
            string res = "";
            int current = user.GetEquippedEquip();
            while(reader.Read())
            {
                int sid = (int)reader["sid"];
                int level = (int)reader["level"];
                int id=(int)reader["id"];
                var equip = EquipFactory.GenerateEquip(sid, level, id);
                if(id==current)
                {
                    res += '*';
                }
                res+=id+" "+equip.name+" "+equip.quality.ToString()+" 等级"+equip.level+'\n';
            }
            reader.Close();
            if(res.Length==0)
            {
                res += "你当前没有装备哦。\n输入【捡垃圾】可以随机获得r或sr装备\n输入【香霖堂】可以购买装备。";
            }
            res += "输入【查看装备 序号】查看装备详情\n";
            res += "输入【装备 序号】装备上目标装备\n";
            res += "输入【一键熔铸】将背包里所有未装备的r级装备朝着5级r装备强化";
            new Sender().QuicklyReply(user.group, res);
        }
        public static void CkeckEquip(User user,string param)
        {
            string[] para = param.Split(" ");
            if(para.Length>1)
            {
                if (int.TryParse(para[1],out int id))
                {
                    string cmd = "select * from equipdata where qq='" + user.qq + "' and id=" + id;
                    var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(cmd);
                    if(reader.Read())
                    {
                        var equip = EquipFactory.GenerateEquip((int)reader["sid"], (int)reader["level"],id);
                        new Sender().QuicklyReply(user.group, equip.Introduce());
                    }
                    else
                    {
                        new Sender().QuicklyReply(user.group, "这个id的装备不存在。");
                    }
                    reader.Close();
                }
            }
        }
        public static void Equiping(User user,string param)
        {
            string[] para = param.Split(" ");
            if (para.Length > 1)
            {
                if (int.TryParse(para[1], out int id))
                {
                    user.SetEquip(id);
                }
            }
        }
        public static void OneKeyForge(User user)
        {
            Queue<int>[] level = new Queue<int>[6];
            for(int i=0;i<level.Length;i++)
            {
                level[i] = new Queue<int>();
            }
            int equipped = user.GetEquippedEquip();
            string cmd = "select * from equipdata where qq='" + user.qq +"'";
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(cmd);
            while(reader.Read())
            {
                Equip temp = EquipFactory.GenerateEquip((int)reader["sid"], (int)reader["level"], (int)reader["id"]);
                if(temp.quality==Equip.Quality.R&&temp.id!=equipped&&temp.level!=5)
                {
                    level[temp.level].Enqueue(temp.id);
                }
            }
            reader.Close();
            List<int> deleteId = new List<int>();
            for(int i=1;i<5;i++)
            {
                while (level[i].Count>=2)
                {
                    int one = level[i].Dequeue();
                    int two = level[i].Dequeue();
                    deleteId.Add(two);
                    level[i + 1].Enqueue(one);
                }
            }
            for(int i=1;i<=5;i++)
            {
                if(level[i].Count==0)
                {
                    continue;
                }
                string sql = "update equipdata set level="+i+" where id in(";
                while (level[i].Count>0)
                {
                    sql +=  level[i].Dequeue()+",";
                }
                sql=sql.Remove(sql.Length-1,1);
                sql += ')';
                Console.WriteLine(sql);
                new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(sql);
            }
            if(deleteId.Count!=0)
            {
                string sqlcmd = "delete from equipdata where id in(";
                foreach(var i in deleteId)
                {
                    sqlcmd = sqlcmd+i + ',';
                }
                sqlcmd=sqlcmd.Remove(sqlcmd.Length-1,1);
                sqlcmd += ')';
                Console.WriteLine(sqlcmd);
                new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(sqlcmd);
            }
            new Sender().QuicklyReply(user.group,"你已一键熔铸成功。");
            Console.WriteLine("success");
        }
    }
}

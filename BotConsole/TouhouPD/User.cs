using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Wife;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class User
    {
        public string qq;
        public string group;
        public int money;
        public DateTime signtime;
        public DateTime operatetime;
        public TimeSpan coldtime;
        private string wives;
        private string equipment;
        public int power;
        public bool tutorial;
        public User(string qq,string group)
        {
            this.qq = qq;
            this.group = group;
            UpdateFromDB();
        }
        public bool Sign()
        {
            bool res;
            if(signtime.Day==DateTime.Now.Day)
            {
                res = false;
            }
            else
            {
                res = true;
                signtime = DateTime.Now;
            }
            UpdateToDB();
            return res;
        }
        public void GetMoney(int amount)
        {
            money += amount;
            UpdateToDB();
        }
        public bool CostMoney(int amount)
        {
            bool res;
            if(money >=amount)
            {
                new Sender().QuicklyReply(group, "成功消耗" + amount + "円");
                money-= amount;
                res=true;
            }
            else
            {
                new Sender().QuicklyReply(group, "你的余额不足了，仅剩下"+money+"円");
                res=false;
            }
            UpdateToDB();
            return res;
        }
        private void UpdateFromDB()
        {
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search("select * from userdata" +
                " where qq='" + qq + "'");
            if (reader.Read())
            {
                money = (int)reader["money"];
                signtime = DateTime.Parse(reader["signtime"].ToString());
                operatetime = DateTime.Parse(reader["operatetime"].ToString());
                coldtime = TimeSpan.Parse(reader["coldtime"].ToString());
                wives = reader["wives"].ToString();
                equipment = reader["equipment"].ToString();
                power = (int)reader["power"];
                tutorial = (bool)reader["tutorial"];
            }
            else
            {
                money = 0;
                signtime = DateTime.MinValue;
                operatetime = DateTime.MinValue;
                coldtime = TimeSpan.Zero;
                wives = "";
                equipment = "";
                new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(
                    string.Format("insert into userdata (qq,money,signtime,operatetime,coldtime" +
                    ",wives,equipment) values('{0}',{1},'{2}','{3}','{4}','{5}','{6}')", qq, money,
                    signtime.ToString(), operatetime.ToString(), coldtime.ToString(), wives, equipment));
            }
            reader.Close();
        }
        private void UpdateToDB()
        {
            new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(
                    string.Format("update userdata set money={0},signtime='{1}',operatetime='{2}'," +
                    "coldtime='{3}',wives='{4}',equipment='{5}',power={7},tutorial={8}" +
                    " where qq='{6}'", money, signtime.ToString(),
                     operatetime.ToString(), coldtime.ToString(), wives, equipment,qq,power,tutorial));
        }
        public bool OwnedWife(int id)
        {
            bool res = false;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(String.Format(
                "select level from ownedwife where qq='{0}' and id={1}",qq,id));
            if(reader.HasRows)
            {
                res = true;
            }
            return res;
        }
        public void ShowWifeDetail(int id)
        {
            string sql = "select * from ownedwife where qq='"+qq+"' and id="+id;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            if(reader.HasRows)
            {
                reader.Read();
                int level = (int)reader["level"];
                int exp = (int)reader["exp"];
                var wife = WifeFactory.GenerateWife(id, level,exp);
                //GetEquip(GetEquippedEquip()).Equipping(wife);
                string img = wife.imgUrl;
                string imgcq = CQUtil.QuickGetCQ("image","file="+img+",subType=0");
                string res = "";
                res += imgcq;
                res += string.Format("名字：{0}\n老婆简介：{1}\n" +
                    "【基础属性】\n等级：{17}\n经验：{18}/{19}\n生命值：{2}\n法力值：{3}\n" +
                    "攻击力：{4}\n法术强度：{5}\n物理防御：{6}\n法术防御：{7}\n速度：{8}\n【技能信息】\n" +
                    "[被动]：{9}\n{10}\n[技能1]：{11}\n{12}\n[技能2]：{13}\n{14}\n[技能3]：{15}\n{16}\n"
                    ,wife.name,wife.description,wife.maxHpBase,wife.maxMpBase,wife.attackBase,
                    wife.magicBase, wife.defendBase, wife.mdefendBase, wife.speedBase, wife.skillTitle[0]
                    ,wife.skillDescription[0], wife.skillTitle[1],wife.skillDescription[1]
                    , wife.skillTitle[2], wife.skillDescription[2]
                    , wife.skillTitle[3], wife.skillDescription[3],wife.level,wife.currentExp,wife.level*100);
                var task=new Sender().QuicklyReplyAsync(group, res);
            }
            else
            {
                new Sender().QuicklyReply(group, "错误的id，是不是没抽到这个老婆呢？");
            }
            reader.Close();
        }
        public void SetConfront(int id)
        {
            string sql = "select * from ownedwife where qq='" + qq + "' and id=" + id;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            reader.Read();
            if (reader.HasRows)
            {
                wives=id.ToString();
                UpdateToDB();
                new Sender().QuicklyReply(group,"设为出战成功。");
            }
            reader.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>是否因没有老婆而添加成功</returns>
        public bool AddWife(int id)
        {
            bool res=!OwnedWife(id);
            if(res)
            {
                string sql = string.Format("insert into ownedwife (qq,id,level,nickname) values" +
                    "('{0}',{1},{2},'{3}')",qq,id,1,"");
                new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(sql);
            }
            return res;
        }
        /// <summary>
        /// 确保该老婆存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回是否升级成功，为假则意为满级。</returns>
        public bool LevelUpWife(int id)
        {
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(String.Format(
                "select level from ownedwife where qq='{0}' and id={1}", qq, id));
            reader.Read();
            int level = (int)reader["level"];
            reader.Close();
            var wife = WifeFactory.GenerateWife(id,level);
            if(!wife.LevelUp())
            {
                return false;
            }
            string cmd = "update ownedwife set level="+wife.level+" where qq='"+qq+"' and id="+id;
            new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(cmd); 
            return true;
        }
        /// <summary>
        /// 获得出战老婆的id
        /// </summary>
        /// <returns>不存在返回-1</returns>
        public int GetConfront()
        {
            int confront = -1;
            if(!int.TryParse(wives, out confront))
            {
                return -1;
            }
            return confront;
        }
        public int GetConfrontLevel()
        {
            int level = 0;
            string sql = "select * from ownedwife where qq='" + qq + "' and id=" + GetConfront();
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            if (reader.Read())
            {
                level = (int)reader["level"];
            }
            reader.Close();
            return level;
        }
        /// <summary>
        /// 应确保confront存在再调用这个
        /// </summary>
        /// <param name="amount"></param>
        public void ExpConfront(int amount)
        {
            int id = GetConfront();
            string sql = string.Format("select * from ownedwife where qq='{0}'",qq);
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            string cmd = "replace into ownedwife (qq,id,level,exp) values ";
            while(reader.Read())
            {
                var level = (int)reader["level"];
                var exp = (int)reader["exp"];
                var cid = (int)reader["id"];
                var wife = WifeFactory.GenerateWife(cid, level, exp);
                if (cid==id)
                {
                    wife.GetExp(amount);
                }
                else
                {
                    wife.GetExp(amount * 3 / 10);
                }
                cmd += string.Format("('{0}',{1},{2},{3}),",qq,cid,wife.level,wife.currentExp);
            }           
            reader.Close();
            cmd = cmd.Remove(cmd.Length-1);
            new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(cmd);
        }
        public void Exercise()
        {
            if(DateTime.Now - operatetime > coldtime)
            {
                var id = GetConfront();
                if(id!=-1)
                {
                    var exp = new Random().Next(500, 1000);
                    operatetime = DateTime.Now;
                    TimeSpan span = TimeSpan.FromMinutes(new Random().Next(10,40));
                    TimeSpan secondspan = TimeSpan.FromSeconds(new Random().Next(0, 59));
                    span += secondspan;
                    coldtime = span;
                    ExpConfront(exp);
                    new Sender().QuicklyReply(group,"成功给出战老婆"+exp+"经验，冷却"+span.ToString()
                        +"。下次操作的时间是" + PrintSuccessTime());                   
                    UpdateToDB();
                }
                else
                {
                    new Sender().QuicklyReply(group, "错误！你还没有设置出战老婆。");
                }
            }
            else
            {
                var rest = coldtime - (DateTime.Now - operatetime);
                new Sender().QuicklyReply(group, "还在冷却中！" +
                    "\n冷却完成时间：" + PrintSuccessTime());
            }
        }
        public void Beg()
        {
            if (DateTime.Now-operatetime>coldtime)
            {
                var money = new Random().Next(500, 800);
                operatetime = DateTime.Now;
                TimeSpan span = TimeSpan.FromMinutes(new Random().Next(10, 40));
                TimeSpan secondspan = TimeSpan.FromSeconds(new Random().Next(0, 59));
                span += secondspan;
                coldtime = span;
                new Sender().QuicklyReply(group, "成功获得" + money + "円，冷却" + span.ToString()
                    + "。下次操作的时间是" + PrintSuccessTime());              
                GetMoney(money);                
            }
            else
            {
                var rest = coldtime - (DateTime.Now - operatetime);
                new Sender().QuicklyReply(group, "还在冷却中！" +
                    "\n冷却完成时间："+ PrintSuccessTime());
            }
        }
        public void CollectRub()
        {
            if (DateTime.Now - operatetime > coldtime)
            {
                int random = new Random().Next(100);
                int id;
                string res = "恭喜你获得了一把";
                operatetime = DateTime.Now;
                TimeSpan span = TimeSpan.FromMinutes(new Random().Next(10, 40));
                TimeSpan secondspan= TimeSpan.FromSeconds(new Random().Next(0, 59));
                span += secondspan;
                coldtime = span;
                if (random<90)
                {
                    id = EquipFactory.RandomQuality(Equip.Quality.R);
                    res += "R级的";
                }
                else
                {
                    id = EquipFactory.RandomQuality(Equip.Quality.SR);
                    res += "SR级的";
                }
                var equip = EquipFactory.GenerateEquip(id, 1, 0);
                res += equip.name+"!\n冷却"+ span.ToString() + 
                    "。下次操作的时间是" +
                    PrintSuccessTime();               
                string cmd = string.Format("insert into equipdata (qq,sid,level) values('{0}',{1},1)",qq,id);
                new DBMgr("erogemanager","a1935515130","botuserdata").Execute(cmd);
                new Sender().QuicklyReply(group, res);
                UpdateToDB();
            }
            else
            {
                var rest = coldtime - (DateTime.Now - operatetime);
                new Sender().QuicklyReply(group, "还在冷却中！" +
                    "\n冷却完成时间：" + PrintSuccessTime());
            }
        }
        public string PrintSuccessTime()
        {
            DateTime time = operatetime + coldtime;
            /*var year = NumToChina(time.Year.ToString());
            var month = NumToChina(time.Month.ToString());
            var day=NumToChina(time.Day.ToString());
            var hour=NumToChina(time.Hour.ToString());
            var minute=NumToChina(time.Minute.ToString());
            var second=NumToChina(time.Second.ToString());*/
            return time.ToLongTimeString();
        }
        public void AddEquip(int sid,int level)
        {
            string cmd = string.Format("insert into equipdata (qq,sid,level) values('{0}',{1},{2})", qq, sid,level);
            new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(cmd);
        }
        public void SetEquip(int id)
        {
            string sql = "select * from equipdata where qq='" + qq + "' and id=" + id;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            reader.Read();
            if (reader.HasRows)
            {
                equipment = id.ToString();
                UpdateToDB();
                new Sender().QuicklyReply(group, "装备成功！");
            }
            reader.Close();
        }
        public int GetEquippedEquip()
        {
            int equip = -1;
            if (!int.TryParse(equipment, out equip))
            {
                return -1;
            }
            return equip;
        }
        /// <summary>
        /// 确保使用这个方法之前，id是存在的。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Equip GetEquip(int id)
        {
            string sql = "select * from equipdata where qq='" + qq + "' and id=" + id;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            reader.Read();
            int sid = (int)reader["sid"];
            int iid = id;
            int level = (int)reader["level"];
            var equip = EquipFactory.GenerateEquip(sid, level, iid);
            reader.Close();
            return equip;
        }
        public bool ExistEquip(int id)
        {
            bool res = false;
            string sql = "select * from equipdata where qq='" + qq + "' and id=" + id;
            var reader = new DBMgr("erogemanager", "a1935515130", "botuserdata").Search(sql);
            if(reader.Read())
            {
                res = true;
            }
            reader.Close();
            return res;
        }
        public void DeleteEquip(int id)
        {
            string sql="delete from equipdata where qq='"+qq+"' and id=" + id;
            if(id==GetEquippedEquip())
            {
                equipment = "";
                UpdateToDB();
            }
            new DBMgr("erogemanager", "a1935515130", "botuserdata").Execute(sql);
        }
        public bool PowerReduce()
        {
            if(power>0)
            {
                power--;
                UpdateToDB();
                return true;
            }
            return false;
        }
        public void ResetPower()
        {
            power = 3;
            UpdateToDB();
        }
        public bool Tutorial()
        {
            if(!tutorial)
            {
                tutorial = true;
                UpdateToDB();
                return false;
            }
            return true;
        }
        #region  数字转汉字
        public string NumToChina(string num)
        {
            if (num.IndexOf(".") == -1)//只有整数部分
            {
                if (num.Length > 12)
                    return "";
                else
                    return Part_Int(num);
            }
            else
            {
                if (num.Length > 14)
                    return "";
                else
                    return Part_Int(num.Split('.')[0]) + Part_Decimal(num.Split('.')[1]);
            }
        }
        public string Part_Int(string A)//整数部分处理
        {
            if (string.IsNullOrEmpty(A))
                return "";
            string[] Units = new string[] { "", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };//12个量词
            string A_rev = String.Join("", A.Reverse());
            string tmp = "";
            for (int i = A.Length - 1; i >= 0; i--)
            {
                tmp += DitoCn(A_rev[i], i) + Units[i];//数字转汉字，并加上量词
            }
            return tmp.Replace("零仟", "零").Replace("零佰", "零").Replace("零拾", "零").Replace("零零零#万", "零").Replace("零零零", "零").Replace("零零", "零").Replace("零#", "").Replace("#", "");
        }

        public string Part_Decimal(string B)//小数部分处理
        {
            if (string.IsNullOrEmpty(B))
                return "";
            B = B.TrimEnd('0');
            int lens = B.Length;
            if (lens == 0 || lens > 2)
                return "";

            string tmp = "点";
            for (int i = 0; i < lens; i++)
                tmp += DitoCn(B[i]);
            return tmp;
        }
        public string DitoCn(char cha, int index = -1)//数字转汉字
        {
            string tmp = "";
            switch (cha)
            {
                case '0':
                    if (index == 0 || index == 4 || index == 8)
                        tmp = "#";
                    else
                        tmp = "零";
                    break;
                case '1':
                    tmp = "壹";
                    break;
                case '2':
                    tmp = "贰";
                    break;
                case '3':
                    tmp = "叁";
                    break;
                case '4':
                    tmp = "肆";
                    break;
                case '5':
                    tmp = "伍";
                    break;
                case '6':
                    tmp = "陆";
                    break;
                case '7':
                    tmp = "柒";
                    break;
                case '8':
                    tmp = "捌";
                    break;
                case '9':
                    tmp = "玖";
                    break;
                default:
                    tmp = cha.ToString();
                    break;
            }
            return tmp;
        }

        #endregion
    }
}

using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class Illustrated
    {
        public static Dictionary<int, Type> wifelist = WifeFactory.id2Name;
        public static Dictionary<int, Type> Rare = EquipFactory.Rare;
        public static Dictionary<int, Type> SRare = EquipFactory.SRare;
        public static Dictionary<int, Type> SSRare = EquipFactory.SSRare;
        public static void CkeckIllustrated(User user)
        {
            string res = "输入【老婆图鉴】查看现有老婆\n输入【装备图鉴】查看现有装备";
            new Sender().QuicklyReply(user.group, res);
        }
        public static void WifeIllustrated(User user,string param)
        {
            if(param.Equals("老婆图鉴"))
            {
                string res = "";
                foreach(var i in wifelist)
                {
                    var w = WifeFactory.GenerateWife(i.Key, 1);
                    res += w.id + " " + w.name + '\n';
                }
                res += "请输入【老婆图鉴 序号 等级】查看老婆详细信息。";
                new Sender().QuicklyReply(user.group, res);
                return;
            }
            string[] para = param.Split(" ");
            if(para.Length==2)
                if (int.TryParse(para[1],out int number))
                {
                    if(!wifelist.ContainsKey(number))
                    {
                        return;
                    }
                    var init = WifeFactory.GenerateWife(number, 1);
                    var full = WifeFactory.GenerateWife(number, 100);
                    string intro = CQUtil.QuickGetCQ("image", "file=" + init.imgUrl + ",subType=0");
                    intro += init.name + '\n';
                    intro += "【基础属性】1级/100级\n";
                    intro += string.Format("生命：{0}/{1}\n法力值：{2}/{3}\n攻击力：{4}/{5}\n法术强度：{6}/{7}\n" +
                        "防御力：{8}/{9}\n法术防御：{10}/{11}\n速度：{12}/{13}\n",init.maxHpBase,full.maxHpBase,
                        init.maxMpBase,full.maxMpBase,init.attackBase,full.attackBase,init.magicBase,
                        full.magicBase,init.defendBase,full.defendBase,init.mdefendBase,full.mdefendBase,
                        init.speedBase,full.speedBase);
                    for(int i=0;i<4;i++)
                    {
                        intro += "【" + init.skillTitle[i] + "】：" + init.skillDescription[i] + '\n';
                    }
                    new Sender().QuicklyReply(user.group,intro);
                }
            if(para.Length==3)
            {
                if (!int.TryParse(para[1], out int number))
                {
                    return;
                }
                if (!int.TryParse(para[2],out int level))
                {
                    return;
                }
                var wife= WifeFactory.GenerateWife(number, level);
                var res = CQUtil.QuickGetCQ("image", "file=" + wife.imgUrl + ",subType=0");
                res += string.Format("名字：{0}\n老婆简介：{1}\n" +
                    "【基础属性】\n等级：{17}\n生命值：{2}\n法力值：{3}\n" +
                    "攻击力：{4}\n法术强度：{5}\n物理防御：{6}\n法术防御：{7}\n速度：{8}\n【技能信息】\n" +
                    "[被动]：{9}\n{10}\n[技能1]：{11}\n{12}\n[技能2]：{13}\n{14}\n[技能3]：{15}\n{16}\n"
                    , wife.name, wife.description, wife.maxHpBase, wife.maxMpBase, wife.attackBase,
                    wife.magicBase, wife.defendBase, wife.mdefendBase, wife.speedBase, wife.skillTitle[0]
                    , wife.skillDescription[0], wife.skillTitle[1], wife.skillDescription[1]
                    , wife.skillTitle[2], wife.skillDescription[2]
                    , wife.skillTitle[3], wife.skillDescription[3], wife.level);
                new Sender().QuicklyReply(user.group, res);

            }
        }
        public static void EquipIllustrated(User user, string param)
        {
            if(param.Equals("装备图鉴"))
            {
                string res = "";
                res += "【R级装备】\n";
                foreach (var i in Rare)
                {
                    var e=EquipFactory.GenerateEquip(i.Key,1,0);
                    res += i.Key + " " + e.name + '\n';
                }
                res += "【SR级装备】\n";
                foreach (var i in SRare)
                {
                    var e = EquipFactory.GenerateEquip(i.Key, 1, 0);
                    res += i.Key + " " + e.name + '\n';
                }
                res += "【SSR级装备】\n";
                foreach (var i in SSRare)
                {
                    var e = EquipFactory.GenerateEquip(i.Key, 1, 0);
                    res += i.Key + " " + e.name + '\n';
                }
                res += "请输入【装备图鉴 序号】查看装备详细信息。";
                new Sender().QuicklyReply(user.group, res);
                return;
            }
            string[] para = param.Split(" ");
            if (para.Length > 1)
                if (int.TryParse(para[1], out int number))
                {
                    Dictionary<int, Type> temp = null;
                    if (Rare.ContainsKey(number)) temp = Rare;
                    if (SRare.ContainsKey(number)) temp = SRare;
                    if (SSRare.ContainsKey(number)) temp = SSRare;
                    if(temp==null)
                    {
                        return;
                    }
                    temp.TryGetValue(number, out var type);
                    Equip full = (Equip)System.Activator.CreateInstance(type);
                    full.SetLevel(5);
                    string res = "5级装备预览\n";
                    res += full.Introduce();
                    new Sender().QuicklyReply(user.group, res);
                }
        }
    }
}

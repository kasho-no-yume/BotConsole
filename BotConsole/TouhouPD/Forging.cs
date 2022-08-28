using BotConsole.TouhouPD.Equipment;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class Forging
    {
        public static void Explain(User user)
        {
            string description = "在这里，可以让你锻出更完美的装备。\n" +
                "【装备强化 序号 序号】前面一个序号写被强化装备，后一个序号写用作素材的装备。使前一个装备的" +
                "等级+1。注意，强化装备和素材装备的等级和阶级必须一致。\n" +
                "【装备升阶 序号 序号】将两个满级的低阶装备融合为一个随机的高一阶装备。\n" +
                "【装备重锻 序号】将装备重新锻造为同品质的另一个随机装备，重锻后装备等级-1。因此1级装备无法" +
                "重锻。";
            new Sender().QuicklyReply(user.group,description);
        }
        public static void Strengthen(User user,string param)
        {
            string[] para = param.Split(" ");
            if(para.Length>2)
            {
                int target, material;
                if(int.TryParse(para[1], out target) && int.TryParse(para[2],out material))
                {
                    if(user.ExistEquip(target)&&user.ExistEquip(material))
                    {
                        var t = user.GetEquip(target);
                        var m = user.GetEquip(material);
                        if(t.quality==m.quality&&t.level==m.level)
                        {
                            if(t.level!=t.maxLevel)
                            {
                                if (user.GetEquippedEquip() == material)
                                {
                                    new Sender().QuicklyReply(user.group, "作为素材的装备已经装备。不能被消耗。");
                                    return;
                                }
                                user.DeleteEquip(material);
                                string sql = "update equipdata set level="+(t.level+1)+" where qq='"+user.qq+
                                    "' and id="+target;
                                new DBMgr("erogemanager","a1935515130","botuserdata").Execute(sql);
                                new Sender().QuicklyReply(user.group, "装备强化成功！");
                                return;
                            }
                            new Sender().QuicklyReply(user.group, "装备已经满级！");
                            return;
                        }
                        new Sender().QuicklyReply(user.group, "装备品质或等级不一样！");
                        return;
                    }
                    new Sender().QuicklyReply(user.group,"有不存在的id！");
                    return;
                }
            }
        }
        public static void Upgrade(User user, string param)
        {
            string[] para = param.Split(" ");
            if (para.Length > 2)
            {
                int target, material;
                if (int.TryParse(para[1], out target) && int.TryParse(para[2], out material))
                {
                    if (user.ExistEquip(target) && user.ExistEquip(material))
                    {
                        var t = user.GetEquip(target);
                        var m = user.GetEquip(material);
                        if(t.quality!=Equip.Quality.SSR&&m.quality!=Equip.Quality.SSR)
                        {
                            if(t.quality==m.quality)
                            {
                                if(t.level==t.maxLevel&&m.level==m.maxLevel)
                                {
                                    if(user.GetEquippedEquip()==material)
                                    {
                                        new Sender().QuicklyReply(user.group,"作为素材的装备已经装备。不能被消耗。");
                                        return;
                                    }
                                    var q = t.quality + 1;
                                    user.DeleteEquip(target);
                                    user.DeleteEquip(material);
                                    var sid=EquipFactory.RandomQuality(q);
                                    user.AddEquip(sid, 1);
                                    string res = "恭喜你锻造出" + EquipFactory.GenerateEquip(sid, 1, 0).name + "!";
                                    new Sender().QuicklyReply(user.group,res);
                                    return;
                                }
                                new Sender().QuicklyReply(user.group, "都没满级就想升阶？");
                                return;
                            }
                            new Sender().QuicklyReply(user.group, "装备品质都不一样，怎么升阶。");
                            return;
                        }
                        new Sender().QuicklyReply(user.group, "这也太土豪了吧，满品质装备也拿来升阶。没地升啊。");
                        return;
                    }
                    new Sender().QuicklyReply(user.group, "有不存在的id！");
                    return;
                }
            }
        }
        public static void Reforging(User user, string param)
        {
            string[] para = param.Split(" ");
            if (para.Length < 1)
            {
                return;
            }
            int target;
            if (!int.TryParse(para[1], out target))
            {
                return;
            }
            if (!user.ExistEquip(target))
            {
                new Sender().QuicklyReply(user.group, "不存在的装备id！");
                return;
            }
            var t = user.GetEquip(target);
            if(t.level<=1)
            {
                new Sender().QuicklyReply(user.group, "装备等级不够！");
                return;
            }
            var q = t.quality;
            var level = t.level - 1;
            user.DeleteEquip(target);
            /*if (user.GetEquippedEquip() == target)
            {
                
            }*/
            var sid = EquipFactory.RandomQuality(q);
            user.AddEquip(sid, level);
            string res = "恭喜你重锻出" + EquipFactory.GenerateEquip(sid, level, 0).name + "!";
            new Sender().QuicklyReply(user.group, res);
            return;
        }
    }
}

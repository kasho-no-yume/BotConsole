using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Gamer;
using BotConsole.TouhouPD.Gamer.BotStrategy;
using BotConsole.TouhouPD.Wife;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class SinglePlay
    {
        private static DateTime today;
        private static string todayBounsType;
        static SinglePlay()
        {
            today = DateTime.Now;
            Refresh();
        }
        private static void Refresh()
        {
            string[] reward = new string[] { "exp", "equip", "money" };
            todayBounsType = reward[new Random().Next(3)];
        }
        public SinglePlay()
        {
            if(today.Day!=DateTime.Now.Day)
            {
                Refresh();
            }
        }
        public static void ClearMutant(User user,string param)
        {
            string[] para = param.Split(" ");
            if(param.Equals("清除异变"))
            {
                string res = "人机对战模式。在这里清除异变说不定能获得丰厚的报酬哦？\n";
                res += "新手教程 （首通送随机sr装备！）\n";
                res += "**挑战[1~10的数字] 消耗体力送大量资源！(**是金币，经验，装备3个中的一个)\n";
                res += "test[1~100的数字] 战斗测试，不耗体力。数字是机器人等级。\n";
                res += "custom [己方老婆id] [己方老婆等级] [敌方老婆id] [敌方老婆等级]" +
                    " 自定义测试，至少要有4个老婆参数。" +
                    "人机只会普攻。主要是用来测试自己用的老婆的。";
                res += "输入【清除异变 标题】消耗一点体力进行挑战！体力在每次签到后会回复到3点。\n";
                res += "例：【清除异变 每日挑战10】，奖励10倍，但会面对基数10倍等级的强力黑幕！\n";
                res += "你当前有" + user.power + "点体力。";
                new Sender().QuicklyReply(user.group,res);
            }
            if(para.Length >1)
            {
                if (PlayingQQ.IsPlaying(user.qq))
                {
                    new Sender().QuicklyReply(user.group, "你已经在玩了，不要一心二用。");
                    return;
                }
                if (user.GetConfront() == -1)
                {
                    new Sender().QuicklyReply(user.group, "你没配置出战老婆还想打？");
                    return;
                }
                if (para[1].Contains("test"))
                {
                    if (para[1].Length == 4)
                    {
                        return;
                    }
                    string level = para[1].Substring(4);
                    if (!int.TryParse(level, out int levelres))
                    {
                        return;
                    }
                    if (levelres < 1 || levelres > 100)
                    {
                        return;
                    }
                    PlayingQQ.Playing(user.qq);
                    var gamer = new GamePlayer(user);
                    var wife = WifeFactory.GenerateWife(1009,levelres);
                    var equip = EquipFactory.GenerateEquip(3, 5, 0);
                    var bot = new GameBot("测试用机器人", wife, equip, new FlanWeaponStra(wife));
                    new BattleRoom(gamer, bot).GameStart();
                }
                if (para[1].Equals("custom"))
                {
                    if(para.Length<6)
                    {
                        return;
                    }
                    if (!int.TryParse(para[2],out int selfid))
                    {
                        return;
                    }
                    if (!int.TryParse(para[2], out int selflevel))
                    {
                        return;
                    }
                    if (!int.TryParse(para[2], out int oppoid))
                    {
                        return;
                    }
                    if (!int.TryParse(para[2], out int oppolevel))
                    {
                        return;
                    }
                    var self = WifeFactory.GenerateWife(selfid, selflevel);
                    var oppo = WifeFactory.GenerateWife(oppoid, oppolevel);
                    if(self==null||oppo==null)
                    {
                        return;
                    }
                    /*if(para.Length>=7)
                    {

                    }*/
                    var gamer = new GamePlayer(user);
                    gamer.wife = self;
                    gamer.weapon = null;
                    var bot = new GameBot("自定义机器人", oppo, null, null);
                    new BattleRoom(gamer, bot).GameStart();
                }
                if (para[1].Contains("挑战"))
                {
                    if (para[1].Length<=4)
                    {
                        return;
                    }
                    string level = para[1].Substring(4);
                    if(!int.TryParse(level,out int levelres))
                    {
                        return;
                    }
                    if(levelres<1||levelres>10)
                    {
                        return;
                    }                    
                    var wife = WifeFactory.GenerateWife(1009, 10 * levelres);
                    var equip = EquipFactory.GenerateEquip(3, ((levelres - 1) / 2) + 1, 0);
                    var bot = new GameBot("每日挑战机器人", wife, equip, new FlanWeaponStra(wife));                   
                    switch (para[1].Substring(0,2))
                    {
                        case "经验":
                            bot.bouns.Add(new KeyValuePair<string, int>("exp", new Random().Next(1500, 1800) * levelres));
                            break;
                        case "金币":
                            bot.bouns.Add(new KeyValuePair<string, int>("money", new Random().Next(1500, 1800) * levelres));
                            break;
                        case "装备":
                            Equip.Quality quality1 = new Random().Next(100) < levelres * levelres ? Equip.Quality.SR : Equip.Quality.R;
                            Equip.Quality quality2 = new Random().Next(100) < levelres * levelres ? Equip.Quality.SR : Equip.Quality.R;
                            Equip.Quality quality3 = new Random().Next(100) < levelres * levelres ? Equip.Quality.SR : Equip.Quality.R;
                            bot.bouns.Add(new KeyValuePair<string, int>("equip", EquipFactory.RandomQuality(quality1)));
                            bot.bouns.Add(new KeyValuePair<string, int>("equip", EquipFactory.RandomQuality(quality2)));
                            bot.bouns.Add(new KeyValuePair<string, int>("equip", EquipFactory.RandomQuality(quality3)));
                            break;
                        default:return;
                    }
                    if (!user.PowerReduce())
                    {
                        new Sender().QuicklyReply(user.group, "你体力不够了。");
                        return;
                    }
                    PlayingQQ.Playing(user.qq);
                    var gamer = new GamePlayer(user);              
                    new BattleRoom(gamer, bot).GameStart();
                }
                if (para[1].Equals("新手教程"))
                {
                    PlayingQQ.Playing(user.qq);
                    var gamer = new GamePlayer(user);
                    var wife = WifeFactory.GenerateWife(1005, 1);
                    //var equip = EquipFactory.GenerateEquip(2, 5, 0);
                    var bot = new GameBot("新手训练机器人", wife, null, null);
                    if (!user.tutorial)
                    {
                        user.Tutorial();
                        bot.bouns.Add(new KeyValuePair<string, int>("equip", EquipFactory.RandomQuality(Equip.Quality.SR)));
                    }
                    List<ForwardMsg> fmsg = new List<ForwardMsg>();
                    fmsg.Add(new ForwardMsg("教程",user.qq,"欢迎阅读本长长的教程，实际上碍于环境所限，我也不知道能说" +
                        "什么，也不知道说些什么好。"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "每次在开始战斗的时候，你都能看到一个提示行动的消息，" +
                        "那是在提醒玩家该做出行动了。当然，机器人行动时也会出现，不过机器人会自动快速地作出响应。"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "需要注意的是，等待玩家做出响应的时间只有两分钟，超时会" +
                        "被判负。"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "提示行动会粗略显示双方的属性信息，帮助判断作出下一步的行动"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "现在来看一下面板所提供的指令。"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "【攻击】在默认的情况下，会对敌方造成1倍攻击力的物理伤害。"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "【防御】会减少30%所受的所有伤害，并回复10%mp"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "【技能】可以查看自己老婆的技能信息，帮助做出回应"));
                    fmsg.Add(new ForwardMsg("教程", user.qq, "【技能123】比如回复技能1就是使用1技能。需要注意的是，有些" +
                        "技能会有吟唱时间。比如1x就是一次行动回合的吟唱时间，2x就是两倍行动回合时间了。有些技能有" +
                        "奇奇怪怪的释放条件，不只是蓝量限制。如果在吟唱中，被削蓝了，到最后技能也不会释放哦。"));
                    fmsg.Add(new ForwardMsg("教程", "", "速度属性决定了出手的顺序。速度越快，能够行动的越快。如果" +
                        "敌我速度差距有10倍的话，理论上快的一方行动10次慢的一方才能行动一次。所以这是很重要的一个属性。"));
                    new Sender().QuicklySendForward(user.group, fmsg);
                    new BattleRoom(gamer, bot).GameStart();
                }
            }
        }
    }
}

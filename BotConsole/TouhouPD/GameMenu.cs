using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class GameMenu
    {
        static string hint = "【签到】 每日限一次，可以领取到随机资源。\n" +
            "【供奉】 消耗円可以加权随机领养老婆，如果重复则该老婆等级加1，若是满级了会全额退款。\n" +
            "【我的老婆】 可以查看管理当前拥有的老婆。\n" +
            "【我的装备】 可以查看管理当前拥有的装备。\n" +
            "【捡垃圾】 可以加权随机获得装备及神器。一日无数次但有冷却时间。与另外需要冷却的操作冲突。\n" +
            "【修行】  增加当前出战老婆的经验，一日无数次但有冷却时间。与另外需要冷却的操作冲突。\n" +
            "【求供奉】 可以获得随机円。一日无数次但有冷却时间。与另外需要冷却的操作冲突。\n" +
            "【变废为宝】 可以对装备进行品质的锻造。\n" +
            "【香霖堂】 商店，消耗円购买可能稀奇的装备。\n" +
            "【清除异变】 人机对战，可以获得各种各样的东西，包括钱，装备，神器，经验和老婆。\n" +
            "【约架 QQ号】  双人老婆约架，需被约方同意。所有操作都有两分钟限时，逾期判负。\n" +
            "【查看财富】 查看自己有多少钱\n" +
            "【图鉴】查看当前的老婆和装备信息。\n";
        public static void ShowMenu(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string msg = json["message"].ToString();
                if (msg.Equals("养老婆"))
                {
                    new Sender().QuicklyReply(json["group_id"].ToString(), hint);
                }
            }
        }
        public static void BroadcastMsg(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                string qq = json["sender"]["user_id"].ToString();
                string group = json["group_id"].ToString();
                var user = new User(qq,group);
                var msg = json["message"].ToString();
                switch (json["message"].ToString().Split(" ")[0])
                {
                    case "签到":Signin.Signing(user);
                        break;
                    case "我的老婆":MyWife.CheckWives(user);
                        break;
                    case "查看老婆":MyWife.WifeDetail(user, msg);
                        break;
                    case "老婆出战": MyWife.SetConfront(user, msg);
                        break;
                    case "查看财富":new Sender().QuicklyReply(group, "你当前拥有："+user.money);
                        break;
                    case "供奉":Draw.Drawing(user);
                        break;
                    case "修行":user.Exercise();break;
                    case "求供奉":user.Beg();break;
                    case "捡垃圾":user.CollectRub();break;
                    case "我的装备":MyEquip.ShowEquipList(user);break;
                    case "查看装备":MyEquip.CkeckEquip(user, msg);break;
                    case "装备": MyEquip.Equiping(user, msg); break;
                    case "装备强化":Forging.Strengthen(user, msg);break;
                    case "装备升阶":Forging.Upgrade(user, msg);break;
                    case "装备重锻":Forging.Reforging(user, msg);break;
                    case "变废为宝":Forging.Explain(user);break;
                    case "香霖堂":Shop.ShowList(user);break;
                    case "刷新商品":Shop.RefreshGoods(user);break;
                    case "购买":Shop.Purchase(user, msg); break;
                    case "查看商品":Shop.CheckGoods(user, msg);break;
                    case "一键熔铸":MyEquip.OneKeyForge(user);break;
                    case "清除异变":SinglePlay.ClearMutant(user, msg);break;
                    case "约架":new MultiplePlay().MakeFight(user, msg); break;
                    case "图鉴":Illustrated.CkeckIllustrated(user);break;
                    case "老婆图鉴":Illustrated.WifeIllustrated(user, msg);break;
                    case "装备图鉴":Illustrated.EquipIllustrated(user, msg);break;
                }
            }
                
        }
    }
}

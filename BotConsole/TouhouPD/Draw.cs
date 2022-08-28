using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class Draw
    {
        //抽奖消耗的钱数
        public static int cost = 1000;
        public static void Drawing(User user)
        {
            if (user.money >= cost)
            {
                user.CostMoney(cost);
                int rid = RandomWife.RandomId();
                var wife = WifeFactory.GenerateWife(rid, 1);
                new Sender().QuicklyReply(user.group, "恭喜你抽到了" + wife.name + "!");
                if (!user.AddWife(rid))
                {
                    if (!user.LevelUpWife(rid))
                    {
                        user.GetMoney(cost);
                        new Sender().QuicklyReply(user.group, "但是因为你该老婆已经满级，所以本次抽奖" +
                            "作废，金额全部返还。");
                        return;
                    }
                    new Sender().QuicklyReply(user.group, "你已经拥有该老婆，所以该老婆等级+1！");
                    return;
                }

            }
            else
            {
                new Sender().QuicklyReply(user.group, "你的钱不够了。");
            }
        }
    }
}

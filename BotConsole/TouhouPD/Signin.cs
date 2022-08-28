using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class Signin
    {
        private static int signinNum=0;
        private static DateTime listTime;
        static Signin()
        {
            listTime = DateTime.Now;
            string cmd = "select signtime from userdata";
            var reader=new DBMgr("erogemanager","a1935515130","botuserdata").Search(cmd);
            while(reader.Read())
            {
                var time = DateTime.Parse(reader["signtime"].ToString());
                if(time.Year==listTime.Year&&time.Month==listTime.Month&&time.Day==listTime.Day)
                {
                    signinNum++;
                }
            }
            reader.Close();
        }
        public static void Signing(User user)
        {           
            if(user.Sign())
            {
                if(DateTime.Now.Day!=listTime.Day)
                {
                    signinNum = 0;
                    listTime = DateTime.Now;
                }
                signinNum++;
                int rank = signinNum;               
                int money = (4800 / (5 + rank));
                if(rank<4)
                {
                    money *= 2;
                }
                var res = "恭喜！你是今天第" + rank + "个签到的！\n" +
                    "获得" + money + "円。\n";
                res += rank <4 ? "因为你抢到了前三，奖励翻倍！" : "";
                user.ResetPower();
                new Sender().QuicklyReply(user.group,res);               
                user.GetMoney(money);
            }
            else
            {
                new Sender().QuicklyReply(user.group, "你今天已经签到过啦，请不要重复签到。");
            }
        }
    }
}

using BotConsole.Delegate;
using BotConsole.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal class MultiplePlay
    {
        private bool MatchOK;
        private string opponentqq;
        private User user;
        public void MakeFight(User user,string param)
        {
            MatchOK = false;
            this.user = user;
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
            string[] para = param.Split(" ");
            if (para.Length > 1)
            {
                string oppoqq = para[1];
                if(CQUtil.IsCQCode(oppoqq))
                {
                    CQUtil.AnalysisCQ(oppoqq).TryGetValue("qq",out oppoqq);
                }
                string sql = "select * from userdata where qq='" + oppoqq + "'";
                var reader = new DBMgr("erogemanager","a1935515130","botuserdata").Search(sql);
                if(reader.Read())
                {
                    reader.Close();
                    if (PlayingQQ.IsPlaying(oppoqq))
                    {
                        new Sender().QuicklyReply(user.group, "你的对手已经在对战拉。");
                        return;
                    }
                    var oppo = new User(oppoqq, user.group);
                    if (oppo.GetConfront()==-1)
                    {
                        new Sender().QuicklyReply(user.group, "你的对手是单身，不约哦。");
                        return;
                    }
                    opponentqq = oppoqq;
                    PlayingQQ.Playing(user.qq);
                    var thread = new Thread(WaitingInvite);
                    thread.Start();
                }
                else
                {
                    reader.Close();
                    new Sender().QuicklyReply(user.group,"你约的是哪个？");
                }
                
            }
        }
        private void WaitingInvite()
        {
            string cq = CQUtil.QuickGetCQ("at", "qq=" + opponentqq);
            string cq1 = CQUtil.QuickGetCQ("at", "qq=" + user.qq);
            string msg = cq + "，" + cq1 + "向你发起了挑战！\n两分钟内回复“接受挑战”来接受挑战。";
            new Sender().QuicklyReply(user.group, msg);
            MsgDelegate.receiveMsg += ListeningInviteDelegate;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(!MatchOK)
            {
                if(sw.ElapsedMilliseconds>=120000)
                {
                    sw.Stop();
                    PlayingQQ.PlayingOver(user.qq);
                    new Sender().QuicklyReply(user.group, cq + "害怕了，并未接受挑战。");
                    return;
                }
            }
            MsgDelegate.receiveMsg -= ListeningInviteDelegate;
            PlayingQQ.Playing(opponentqq);
            new BattleRoom(new Gamer.GamePlayer(user), new Gamer.GamePlayer(new User(opponentqq, user.group))).GameStart();
        }
        private void ListeningInviteDelegate(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                if (!json["sender"]["user_id"].ToString().Equals(opponentqq))
                {
                    return;
                }
                if (json["message"].ToString().Equals("接受挑战"))
                {
                    MatchOK = true;
                }
            }
        }
    }
}

using BotConsole.Delegate;
using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Wife;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer
{
    internal class GamePlayer : Participant
    {
        private bool hasReceived;
        private string operate;
        /// <summary>
        /// 确保传入的，user，有老婆出战！！
        /// 可以不传入wife
        /// </summary>
        /// <param name="user"></param>
        public GamePlayer(User user)
        {
            wife = WifeFactory.GenerateWife(user.GetConfront(), user.GetConfrontLevel());
            this.user = user;
            if(user.GetEquippedEquip()!=-1)
            weapon = user.GetEquip(user.GetEquippedEquip());
            name = user.qq;
            hasReceived=false;
            iden = "player";
        }

        public override string RequireAct()
        {
            operate = "";
            hasReceived = false;
            MsgDelegate.receiveMsg += ListeningDelegate;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(!hasReceived)
            {
                if(sw.ElapsedMilliseconds>=120000)
                {
                    sw.Stop();
                    MsgDelegate.receiveMsg -= ListeningDelegate;
                    return "overtime";
                }
            }
            MsgDelegate.receiveMsg -= ListeningDelegate;
            sw.Stop();
            return operate;
        }
        private void ListeningDelegate(JObject json)
        {
            if (json["message_type"].ToString().Equals("group"))
            {
                if (!json["sender"]["user_id"].ToString().Equals(user.qq))
                {
                    return;
                }
                string msg = json["message"].ToString();
                switch(msg)
                {
                    case "攻击":
                        operate = "attack";
                        break;
                    case "查看技能":
                        operate = "skill";
                        break;
                    case "技能1":
                        operate = "skill1";
                        break;
                    case "技能2":
                        operate = "skill2";
                        break;
                    case "技能3":
                        operate = "skill3";
                        break;
                    case "防御":
                        operate = "defend";
                        break;
                    case "认输":
                        operate = "surrender";
                        break;
                    case "状态":
                        operate = "state";
                        break;
                    case "详细":
                        operate = "detail";
                        break;
                    default:return;
                }
                hasReceived = true;
            }
        }
    }
}

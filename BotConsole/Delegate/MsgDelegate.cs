using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotConsole.RecvMsg;
using BotConsole.TouhouPD;

namespace BotConsole.Delegate
{
    delegate void ReceiveMsg(JObject json);
    internal static class MsgDelegate
    {
        public static ReceiveMsg receiveMsg;
        static MsgDelegate()
        {
            //receiveMsg += Repeat.RecvRepeat;
            receiveMsg += Output.PrintConsole;
            receiveMsg += RandomPic.RandomAcg;
            receiveMsg += GalEroge.ShowGals;
            receiveMsg += Menu.ShowMenu;
            receiveMsg += Eroge.ShowEroges;
            receiveMsg += GameMenu.ShowMenu;
            receiveMsg += GameMenu.BroadcastMsg;
            receiveMsg += YuanToMoney.Transfer;
            receiveMsg += RecvTransfer.Transfer;
        }
    }
}

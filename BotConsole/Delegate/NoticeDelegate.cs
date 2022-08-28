using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.Delegate
{
    delegate void ReceiveNotice(JObject json);
    internal static class NoticeDelegate
    {
        public static ReceiveNotice receiveNotice;
        static NoticeDelegate()
        {
            
        }
    }
}

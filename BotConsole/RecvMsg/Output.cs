using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.RecvMsg
{
    internal class Output
    {
        public static void PrintConsole(JObject json)
        {
            Console.WriteLine(json.ToString());
        }
    }
}

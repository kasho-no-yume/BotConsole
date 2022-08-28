using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal static class PlayingQQ
    {
        private static List<string> playingQQ;
        static PlayingQQ()
        {
            playingQQ = new List<string>();
        }
        public static void Playing(string qq)
        {
            playingQQ.Add(qq);
        }
        public static bool IsPlaying(string qq)
        {
            return playingQQ.Contains(qq);
        }
        public static void PlayingOver(string qq)
        {
            if(IsPlaying(qq))
            playingQQ.Remove(qq);
        }
    }
}

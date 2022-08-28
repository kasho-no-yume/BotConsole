using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.HiddenStar
{
    internal class KomanoAunn:WifeBase
    {
        public static int sid = 2002;
        public static int weight = 1000;
        public KomanoAunn()
        {
            name = "高丽野阿吽";
            id=sid;
            imgUrl = "https://i.postimg.cc/RVzhqLQV/3.png";
            description = "神社的守护神兽。多数时候都在默默守护神社以至于神社巫女都完全不知道阿哞的存在。因为比起" +
                "被夸奖被世人所知，其更喜欢守护带来的满足感。";
            defendBase = 30;
            mdefendBase = 30;
            defendAddition = 2;
            mdefendBase = 2;
            attackBase = 10;
            magicBase= 10;
            maxHpBase = 300;
            hpAddition = 15;
            skillTitle[0] = "守护神兽";
            skillDescription[0] = "每次当阿哞受到伤害时，都会吸收其中30%的伤害积蓄着。当积蓄的伤害超过最大生命值" +
                "的30%时，会释放出来对阿哞造成伤害，并清空积蓄。";
            skillTitle[1] = "阿哞散步";
            skillDescription[1] = "";
            skillTitle[2] = "狛犬回转";
            skillDescription[2] = "";
            skillTitle[3] = "阿哞呼吸";
            skillDescription[3] = "";
        }

        public override bool CanUseOne()
        {
            throw new NotImplementedException();
        }

        public override bool CanUseThree()
        {
            throw new NotImplementedException();
        }

        public override bool CanUseTwo()
        {
            throw new NotImplementedException();
        }
    }
}

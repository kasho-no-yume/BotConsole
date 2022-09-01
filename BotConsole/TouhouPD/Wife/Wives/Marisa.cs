using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives
{
    internal class Marisa:WifeBase
    {
        public static int sid = 2;
        public static int weight = 40;
        public Marisa()
        {
            name = "雾雨魔理沙";
            id = sid;
            imgUrl = "https://i.postimg.cc/9FYYmDQ2/image.png";
            description = "以追求最高最强最华丽的弹幕火力著称的人类魔法使。正如字面意思，其魔法都看起来十分华丽" +
                "而又火力全开，因此非常胜任放烟花的职责。";
            magicBase = 30;
            magicAddition = 3;
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

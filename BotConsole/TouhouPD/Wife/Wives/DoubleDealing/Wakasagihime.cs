using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.DoubleDealing
{
    internal class Wakasagihime : WifeBase
    {
        public static int weight = 0;
        public static int sid = 1500;
        public Wakasagihime()
        {
            imgUrl = "https://i.postimg.cc/nzMrynPm/1.png";
            name = "若鹭姬";
            id = sid;
            description = "温顺的人鱼歌姬，在水里能发挥出比在陆地上更强的力量。但是即便如此似乎也不是很强？";
            /*skillTitle[0] = "栖于淡水的人鱼";
            skillDescription[0] = "若鹭姬的防御下潜到水里，其防御时受到伤害额外再减一半。此外，防御结束从" +
                "水里出来时，下一次伤害额外增加30%，但对应的下一次受到伤害也增加30%。";
            skillTitle[1] = "尾鳍拍击";
            skillDescription[1] = "消耗30mp，吟唱0x。用尾鳍拍击水面，没有伤害但是能帮助自己游的更快。3回合内速度" +
                "增加20%。(buff)";
            skillTitle[2] = "";
            skillDescription[2] = "";
            skillTitle[3] = "人鱼之歌";
            skillDescription[3] = "消耗80mp，吟唱1.5x。以人鱼塞壬之歌迷惑敌方，沉默敌方3回合。并且，在己方3回合内的每一回合，" +
                "若自己未受到伤害，则返回敌方3倍法术强度的伤害；若自己受到伤害，则返还敌方该伤害，自己受到一半伤害。" +
                "冷却5回合。";*/
            for(int i=0;i<4;i++)
            {
                skillTitle[i] = "还没想好";
                skillDescription[i] = "还没想好";
            }
        }
        public override bool CanUseOne()
        {
            return true;
        }

        public override bool CanUseThree()
        {
            return true;
        }

        public override bool CanUseTwo()
        {
            return true;
        }
    }
}

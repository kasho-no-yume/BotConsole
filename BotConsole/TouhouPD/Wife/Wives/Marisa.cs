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
            mpAddition = 2;
            maxMpBase = 80;
            skillTitle[0] = "高火力的魔法使";
            skillDescription[0] = "只有高火力的魔法攻击能击中魔理沙，其受到的所有物理伤害减少其当前剩余法力值点" +
                "数。";
            skillTitle[1] = "灰尘幻想";
            skillDescription[1] = "消耗30%mp，吟唱0.5x。魔理沙逃跑时的绝技，掀起大量灰尘让敌方找不到" +
                "目标。缴械敌方三回合。持续触发刷新缴械时间。";
            skillTitle[2] = "流星破片";
            skillDescription[2] = "消耗10%mp，吟唱0x。胡乱射出星星状的弹幕。造成1到2倍的法术强度的法术伤害。";
            skillTitle[3] = "究极火花";
            skillDescription[3] = "消耗所有mp，吟唱0x。魔理沙的标志性高火力代表。造成法术强度和消耗mp之和2倍的" +
                "法术伤害。此外，因为八卦炉越蓄力越强，使用本技能时每过了1个自己回合伤害额外提升15%。该技能每场战斗" +
                "只能使用一次。";
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

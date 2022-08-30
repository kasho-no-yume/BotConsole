using BotConsole.TouhouPD.Buff;
using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Koakuma : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1005;
        public Koakuma()
        {
            imgUrl = "https://i.postimg.cc/c4bRWjRd/4A.png";
            name = "小恶魔";
            id = 1005;
            maxHpBase = 350;
            hpAddition = 15;
            defendAddition = 3;
            mdefendAddition = 3;
            description = "红魔馆里到处都是小恶魔，扮演着不听话的女仆角色，但长得看起来不像女仆就是了。" +
                "虽说是恶魔，但是因为实力很弱，才有小恶魔之名吧。";
            skillTitle[0] = "恶魔女仆";
            skillDescription[0] = "小恶魔不想参与斗争也不想当女仆，只想混日子。因此小恶魔不会被附加" +
                "任何buff效果。";
            skillTitle[1] = "来点点心吧";
            skillDescription[1] = "消耗30mp，吟唱1x。好吃的点心减缓了敌方的攻击性，敌方攻击力和法术攻击" +
                "减少10%，持续2回合。重复触发叠加效果但不刷新持续时间。";
            skillTitle[2] = "来杯下午茶吧";
            skillDescription[2] = "消耗60mp，吟唱1x。清除敌方所有buff。";
            skillTitle[3] = "该大扫除了";
            skillDescription[3] = "消耗90mp，吟唱1x。清除敌方所有正面buff。";
        }
        public override void AddBuff(BuffBase buff)
        {
            
        }
        public override bool CanUseOne()
        {
            return currentMp >= 30;
        }
        public override bool CanUseTwo()
        {
            return currentMp >= 60;
        }
        public override bool CanUseThree()
        {
            return currentMp>=90;
        }
        public override double GetChantOne()
        {
            return 1;
        }
        public override double GetChantTwo()
        {
            return 1;
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            target.AddBuff(new AttackDown(2, target.attackBase / 10));
            target.AddBuff(new MagicDown(2, target.magicBase / 10));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            for (int i=target.buffList.Count-1;i>=0;i--)
            {
                target.RemoveBuff(target.buffList[i]);
            }
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(90))
            {
                return 0;
            }
            for (int i = target.buffList.Count - 1; i >= 0; i--)
            {
                if (target.buffList[i].effect==BuffBase.Effect.positive)
                target.RemoveBuff(target.buffList[i]);
            }
            return base.SkillThree(target);
        }
    }
}

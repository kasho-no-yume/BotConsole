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
        public int accumulate;
        public int threeCool;
        public int threeRound;
        public KomanoAunn()
        {
            accumulate = 0;
            threeCool = 0;
            threeRound = 0;
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
            hpAddition = 17;
            skillTitle[0] = "守护神兽";
            skillDescription[0] = "每次当阿哞受到伤害时，都会吸收其中50%的伤害积蓄着。当积蓄的伤害超过最大生命值" +
                "的30%时，会使阿哞的生命值流失该值，并清空积蓄。此伤害不纳入积蓄槽。";
            skillTitle[1] = "阿哞散步";
            skillDescription[1] = "消耗30mp，吟唱0x。回复积蓄伤害量一倍的血量，清空积蓄槽。";
            skillTitle[2] = "狛犬回转";
            skillDescription[2] = "消耗40mp，吟唱0x。对敌方造成积蓄伤害量两倍的物理伤害，清空积蓄槽。";
            skillTitle[3] = "阿哞呼吸";
            skillDescription[3] = "消耗60mp，吟唱1.5x。三回合内，积蓄伤害没有上限，不会因为超过最大生命值的30%而" +
                "释放。若在第三回合结束时超过积蓄最大值，则一次释放。冷却5回合。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=30;
        }

        public override bool CanUseThree()
        {
            return currentMp >= 60 && threeCool <= 0;
        }

        public override bool CanUseTwo()
        {
            return currentMp >= 40;
        }
        public override double GetChantThree()
        {
            return 1.5;
        }
        public override string GetState()
        {
            string res = "积蓄伤害量" + accumulate + '\n';
            res += threeCool >=0? "三技能冷却" + threeCool + '\n' : "";
            res += threeRound >= 0 ? "三技能持续" + threeRound + '\n' : "";
            return res+base.GetState();
        }
        public override void RoundStart(WifeBase enemy)
        {
            if(threeRound==0&& accumulate >= maxHpFinal * 3 / 10)
            {
                HpReduce(accumulate);
                accumulate = 0;
            }
            threeCool--;
            threeRound--;
            base.RoundStart(enemy);
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            accumulate += damage / 2;
            if(accumulate>=maxHpFinal*3/10&&threeRound<=0)
            {
                HpReduce(accumulate);
                accumulate=0;
            }
            return base.BeingAttack(attacker, damage/2, type);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            HpGet(accumulate);
            accumulate = 0;
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if (!MpReduce(40))
            {
                return 0;
            }
            int dmg = target.BeingAttack(this, accumulate * 2, DamageType.physical);
            accumulate = 0;
            return dmg;
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            threeRound = 3;
            threeCool = 5;
            return base.SkillThree(target);
        }
    }
}

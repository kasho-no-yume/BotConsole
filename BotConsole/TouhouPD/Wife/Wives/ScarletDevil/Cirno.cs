using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Cirno : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1002;
        public Cirno()
        {
            imgUrl = "https://i.postimg.cc/vTLkMJh3/2B.png";
            name = "琪露诺";
            description = "自恃最强的妖精。拥有着与其实力相符的智力。不过凭借其妖精的身份，却也击败过" +
                "拿出全部实力的秘神？说不定最强并不是吹牛呢。";
            id = 1002;
            speedAddition = 2;
            skillTitle[0] = "湖上冰精";
            skillDescription[0] = "琪露诺总是散发着寒气，受到伤害减少20%。对琪露诺造成伤害时会受到寒气影响，使攻击者" +
                "降低一点速度。此外，琪露诺的普通攻击也能使敌方降低一点速度。";
            skillDescription[1] = "消耗30mp，吟唱0.5x。降低攻击者速度10%，造成法术强度1倍的法术伤害。" +
                "重复使用重复降低但不刷新持续时间。持续三回合。";
            skillTitle[1] = "冰瀑";
            skillTitle[2] = "完美冻结";
            skillDescription[2] = "消耗60mp，吟唱0。冻结攻击者一回合。";
            skillTitle[3] = "钻石风暴";
            skillDescription[3] = "消耗60mp，吟唱1x。释放极强的冰冻之力，造成3倍魔法攻击的魔法伤害。若己方的速度" +
                "高于敌方，则额外提高己方速度高于敌方速度比例的倍数的伤害。";
        }

        public override bool CanUseOne()
        {
            return currentMp >= 30;
        }

        public override bool CanUseThree()
        {
            return currentMp >= 60;
        }

        public override bool CanUseTwo()
        {
            return currentMp >= 60;
        }
        public override double GetChantOne()
        {
            return 0.5;
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override int Attack(WifeBase target)
        {
            target.currentSpeed--;
            return base.Attack(target);
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            damage = damage * 4 / 5;
            attacker.currentSpeed--;
            return base.BeingAttack(attacker, damage, type);
        }
        public override int SkillOne(WifeBase target)
        {
            if (!MpReduce(30))
            {
                return 0;
            }
            target.AddBuff(new SpeedDown(3, target.speedBase / 10));           
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            target.cantActRound = 1;            
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            base.SkillThree(target);
            double rate = 1;
            if(currentSpeed>target.currentSpeed)
            {
                rate = (currentSpeed / target.currentSpeed) ;
            }
            return target.BeingAttack(this,(int)(currentMagic*3*rate),DamageType.magic);
        }
    }
}

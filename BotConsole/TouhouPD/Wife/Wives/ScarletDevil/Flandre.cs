using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Flandre:WifeBase
    {
        public static int weight = 200;
        public static int sid = 1009;
        public int twoCold;
        public int threeCold;
        public bool mustCritical;
        public bool fourExist;
        public Flandre()
        {
            twoCold = 0;
            threeCold = 0;
            mustCritical = false;
            fourExist = false;
            imgUrl = "https://i.postimg.cc/q7c3c0Jk/7-2.png";
            name = "芙兰朵露";
            id = 1009;
            description = "是蕾米莉亚领域外的妹妹，比起蕾米莉亚，芙兰朵露有着更为强大的破坏欲。其本身" +
                "的表现也是趋于癫狂的。但是也能好好听人说话，若是想挑战的话，那几乎是不可能赢的吧。";
            attackBase = 30;
            magicBase = 0;
            magicAddition = 0;
            attackAddition = 3;
            attackPierceRate = 100;
            skillTitle[0] = "鲜血恶魔";
            skillDescription[0] = "拥有着破坏事物根源的极强力量，没有任何防御可以抵挡。芙兰朵露的所有攻击" +
                "都无视防御力(拥有100%物理穿透)。";
            skillTitle[3] = "四重存在";
            skillDescription[3] = "消耗60mp，吟唱1x。下一次伤害4倍。冷却时间5回合。";
            skillTitle[2] = "莱瓦汀";
            skillDescription[2] = "消耗30mp，吟唱0。引出破坏神剑莱瓦汀，自身攻击力提高50%。持续3" +
                "回合。冷却时间5回合。";
            skillTitle[1] = "之后就没有人了吗";
            skillDescription[1] = "消耗30mp，吟唱0。下一次伤害必定暴击。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=30;
        }

        public override bool CanUseThree()
        {
            return currentMp>=60&&threeCold<=0;
        }

        public override bool CanUseTwo()
        {
            return currentMp >= 30 && twoCold <= 0; ;
        }
        public override void RoundStart(WifeBase enemy)
        {
            base.RoundStart(enemy);
            twoCold--;
            threeCold--;
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override int Attack(WifeBase target)
        {
            OnAttackEvent(this, target);
            int damage = currentAttack;
            int finalDmg;
            if(fourExist)
            {
                damage *= 4;
                fourExist = false;
            }
            if(mustCritical)
            {
                criticalFinal += 100;
                finalDmg = target.BeingAttack(this, damage, DamageType.physical);
                criticalFinal -= 100;
                mustCritical = false;
            }
            else
            {
                finalDmg = target.BeingAttack(this, damage, DamageType.physical);
            }
            return finalDmg;
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            mustCritical = true;
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            AddBuff(new AttackUp(3, attackBase / 2));
            twoCold = 5;
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            fourExist = true;
            threeCold = 5;
            return base.SkillThree(target);
        }
    }
}

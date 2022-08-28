using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Sakuya : WifeBase
    {
        public static int weight = 800;
        public static int sid = 1007;
        public bool usedSekai;
        public Sakuya()
        {
            usedSekai = false;
            imgUrl = "https://i.postimg.cc/ZndYc5T0/5.png";
            name = "十六夜咲夜";
            id = 1007;
            description = "实例极强的吸血鬼猎人，被吸血鬼打败后转而为吸血鬼工作。拥有着停止时间的" +
                "无敌能力，但是却在红魔馆当了女仆长。不过其女仆工作做的也十分完美。";
            speedBase = 20;
            skillTitle[0] = "完美的潇洒女仆";
            skillDescription[0] = "每次使用普通飞刀攻击或技能，下一次都会做的更完美。攻击力提高2点，" +
                "可无限叠加。";
            skillTitle[1] = "夜雾幻影杀人鬼";
            skillDescription[1] = "消耗10mp，吟唱0。精确射出一把飞镖，造成1.5倍攻击力的伤害。";
            skillTitle[2] = "月神之钟";
            skillDescription[2] = "消耗30mp，吟唱0。射出8把飞刀，每把能造成1倍攻击力的伤害，但是每把的" +
                "命中率都只有30%。";
            skillTitle[3] = "幻世 世界";
            skillDescription[3] = "消耗90mp，吟唱1x。冻结时间，只有咲夜能够自由行动。敌方停止行动2回合" +
                "，不会打断敌方的吟唱。一场战斗只能使用一次。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=10;
        }

        public override bool CanUseThree()
        {
            return currentMp>=90&&!usedSekai;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=30;
        }
        public override int Attack(WifeBase target)
        {
            currentAttack += 2;
            return base.Attack(target);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(10))
            {
                return 0;
            }
            base.SkillOne(target);
            currentAttack += 2;
            return target.BeingAttack(this,currentAttack*3/2,DamageType.physical);
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            base.SkillTwo(target);
            currentAttack += 2;
            int finalDmg = 0;
            for(int i=0;i<8;i++)
            {
                if(new Random().Next(10)<3)
                {
                    finalDmg += target.BeingAttack(this, currentAttack, DamageType.physical);
                }
            }
            return finalDmg;
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(90))
            {
                return 0;
            }
            target.cantActRound = 2;
            usedSekai = true;
            return base.SkillThree(target);
        }
    }
}

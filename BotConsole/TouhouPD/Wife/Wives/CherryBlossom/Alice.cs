using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.CherryBlossom
{
    internal class Alice:WifeBase
    {
        public static int sid = 1102;
        public static int weight = 1000;
        public int oneCool;
        public int twoCool;
        public Alice()
        {
            oneCool = 0; 
            twoCool = 0;
            name = "爱丽丝";
            imgUrl = "https://i.postimg.cc/vHTgCGsT/3.png";
            id = 1102;
            maxHpBase = 100;
            hpAddition = 9;
            speedBase = 12;
            attackBase = 12;
            magicAddition = 2;
            description = "魔法之森的某魔法使。比起自己使用华丽的魔法，更擅长操作人偶进行战斗。其本人意外的好战，" +
                "向她挑战的话，都不会拒绝。";
            skillTitle[0] = "七色的人偶师";
            skillDescription[0] = "在使用普通攻击的时候，会使在场人偶再攻击一次。";
            skillTitle[1] = "奥尔良人偶";
            skillDescription[1] = "消耗30mp，吟唱0。召唤出奥尔良人偶（以正面buff形式存在），持续5回合，冷却4回合。" +
                "每次回合开始时自动以1.2倍法术强度的魔法伤害攻击敌方。回复造成伤害30%的血量。";
            skillTitle[2] = "上海人偶";
            skillDescription[2] = "消耗30mp，吟唱0。召唤出上海人偶（以正面buff形式存在），持续5回合，冷却4回合。" +
                "每次回合开始时自动以1.2倍法术强度的魔法伤害攻击敌方，并减少敌方8点法术防御。";
            skillTitle[3] = "蓬莱人偶";
            skillDescription[3] = "消耗60mp，吟唱0。召唤出蓬莱人偶，对敌方造成1.5倍法术强度的法术伤害。若自身每" +
                "存在一个人偶，每一个将会额外提高1.5倍的法术强度伤害。释放后会清除自己的人偶。";
        }
        public override string GetState()
        {
            string res = oneCool > 0 ? "一技能冷却" + oneCool + '\n' : "";
            res += twoCool > 0 ? "二技能冷却" + twoCool + '\n' : "";
            return res+base.GetState();
        }
        public override bool CanUseOne()
        {
            return currentMp>=30&&oneCool<=0;
        }

        public override bool CanUseThree()
        {
            return currentMp >= 60;
        }

        public override bool CanUseTwo()
        {
            return currentMp >= 30 && twoCool <= 0;
        }
        public override void RoundStart(WifeBase enemy)
        {
            oneCool--;
            twoCool--;
            SettleDoll(enemy);
            base.RoundStart(enemy);
        }
        public void SettleDoll(WifeBase enemy)
        {
            if(ExistBuff("奥尔良人偶"))
            {
                int dmg=enemy.BeingAttack(this,currentMagic*6/5,DamageType.magic);
                HpGet(dmg * 3 / 10);
            }
            if(ExistBuff("上海人偶"))
            {
                enemy.BeingAttack(this,currentMagic*6/5,DamageType.magic);
                enemy.currentMdefend -= 10;
                if(enemy.currentMdefend<0)
                {
                    enemy.currentMdefend = 0;
                }
            }
        }
        public override int Attack(WifeBase target)
        {
            SettleDoll(target);
            base.Attack(target);
            return target.BeingAttack(this,currentAttack,DamageType.physical);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!CanUseOne())
            {
                return 0;
            }
            MpReduce(30);
            oneCool = 4;
            AddBuff(new Orleans(5));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if (!CanUseTwo())
            {
                return 0;
            }
            MpReduce(30);
            twoCool = 4;
            AddBuff(new Shanghai(5));
            return base.SkillOne(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            double rate = 1.5;
            if (ExistBuff("奥尔良人偶"))
            {
                rate += 1.5;
                RemoveBuff("奥尔良人偶");
            }
            if (ExistBuff("上海人偶"))
            {
                rate += 1.5;
                RemoveBuff("上海人偶");
            }
            base.SkillThree(target);
            return target.BeingAttack(this,(int)(currentMagic*rate),DamageType.magic);
        }
    }
}

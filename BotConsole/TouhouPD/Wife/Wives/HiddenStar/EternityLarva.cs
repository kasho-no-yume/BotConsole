using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.HiddenStar
{
    internal class EternityLarva:WifeBase
    {
        public static int weight = 1000;
        public static int sid = 2000;
        public EternityLarva()
        {
            imgUrl = "https://i.postimg.cc/L89s34bc/1.png";
            id = 2000;
            name = "爱塔妮缇拉尔瓦";
            description = "很温顺的妖精，即使是因为那次事件暴走，也并没有产生攻击性。获得强大力量之后，这样单纯" +
                "的妖精估计只是想着去哪大闹一场的程度吧。";
            skillTitle[0] = "真夏夜的妖精梦";
            skillDescription[0] = "拉尔瓦在感到危险时（被攻击时）会释放臭气，给敌方10点物理伤害，并给敌方施加一层鳞粉" +
                "buff。每拥有一层鳞粉buff，敌方的基础属性全部-1(不包括速度)。";
            skillTitle[1] = "凤蝶的鳞粉";
            skillDescription[1] = "消耗20mp，吟唱0。给敌方施加三层鳞粉buff。";
            skillTitle[2] = "沾身的鳞粉";
            skillDescription[2] = "消耗40mp，吟唱0.5x。给敌方施加两层鳞粉buff。额外回复鳞粉buff层数*20的生命值。";
            skillTitle[3] = "真夏的振翅";
            skillDescription[3] = "消耗40mp，吟唱0.5x。去除敌方所有的鳞粉buff，造成层数一半倍数攻击力的法术伤害";
        }
        public override bool CanUseOne()
        {
            return currentMp>=20;
        }

        public override bool CanUseTwo()
        {
            return currentMp >= 40;
        }

        public override bool CanUseThree()
        {
            return currentMp >= 40;
        }
        public override double GetChantTwo()
        {
            return 0.5;
        }
        public override double GetChantThree()
        {
            return 0.5;
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            attacker.AddBuff(new Scale(100000, 1));
            attacker.BeingAttack(this, 10, DamageType.physical);
            return base.BeingAttack(attacker, damage, type);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(20))
            {
                return 0;
            }
            target.AddBuff(new Scale(100000, 3));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(40))
            {
                return 0;
            }
            target.AddBuff(new Scale(100000, 2));
            foreach(var i in target.buffList)
            {
                if(i.name.Equals("鳞粉"))
                {
                    int level = i.strength;
                    HpGet(level * 20);
                }
            }
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(40))
            { 
                return 0;
            }
            base.SkillThree(target);
            int level = 0;
            foreach (var i in target.buffList)
            {
                if (i.name.Equals("鳞粉"))
                {
                    level = i.strength;
                    HpGet(level * 5);
                }
            }
            target.RemoveBuff("鳞粉");
            return target.BeingAttack(this,level*currentAttack/2,DamageType.magic);
        }
    }
}

using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Remilia : WifeBase
    {
        public static int weight = 200;
        public static int sid = 1008;
        private bool fantasy;
        public Remilia()
        {
            fantasy = false;
            imgUrl = "https://i.postimg.cc/XJVyQhq9/6-2.png";
            id = 1008;
            name = "蕾米莉亚";
            description = "威严满满的红魔馆馆主。虽然身形幼小却是活了超过500年的吸血鬼。表现起来像个" +
                "任性的小孩子，但是其实力是毋庸置疑的高。";
            maxHpBase = 300;
            attackBase = 25;
            magicBase = 0;
            magicAddition = 0;
            attackAddition = 3;
            hpAddition = 15;
            skillTitle[0] = "鲜红恶魔";
            skillDescription[0] = "蕾米莉亚以除buff外任何形式对敌方造成伤害，都将回复伤害量的20%生命值。";
            skillTitle[1] = "吸血鬼幻想";
            skillDescription[1] = "消耗30mp，吟唱0。造成1倍攻击力的伤害，并且使下一次攻击及技能回复" +
                "100%造成伤害的生命值。";
            skillTitle[2] = "红魔法";
            skillDescription[2] = "消耗30%hp，吟唱1x。给敌方施加流血buff，每回合损失攻击力50%的生命值，" +
                "持续3回合。重复使用刷新持续时间。";
            skillTitle[3] = "绯红之主";
            skillDescription[3] = "消耗50%hp，吟唱1x。造成2.5倍攻击力的伤害，额外提高已损失生命值比例" +
                "2倍的伤害。";
        }
        public override string GetState()
        {
            string res = fantasy ? "吸血鬼幻想\n" : "";
            return res+base.GetState();
        }
        public override bool CanUseOne()
        {
            return currentMp>=30;
        }

        public override bool CanUseThree()
        {
            return currentHp >= maxHpFinal / 2;
        }

        public override bool CanUseTwo()
        {           
            return currentHp >= maxHpFinal * 3 / 10;
        }
        public override int Attack(WifeBase target)
        {
            int damage = base.Attack(target);
            if(fantasy)
            {
                fantasy = false;
                HpGet(damage);
            }
            HpGet(damage / 5);
            return damage;
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
            base.SkillOne(target);             
            int damage = target.BeingAttack(this, currentAttack, DamageType.physical);
            HpGet(damage / 5);
            if(fantasy)
            {
                HpGet(damage);
            }
            fantasy = true;
            return damage;
        }
        public override int SkillTwo(WifeBase target)
        {
            if(currentHp<maxHpFinal*3/10)
            {
                return 0;
            }
            currentHp -= maxHpFinal * 3 / 10;
            target.AddBuff(new Bleed(3, currentAttack / 2));
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(currentHp < maxHpFinal / 2)
            {
                return 0;
            }
            currentHp -= maxHpFinal / 2;
            base.SkillThree(target);
            int damage = currentAttack;
            double rate = (1-(currentHp  / maxHpFinal ))*2+2.5;
            damage = (int)(damage * rate);
            int finalDmg = target.BeingAttack(this, damage, DamageType.physical);
            HpGet(finalDmg / 5);
            if(fantasy)
            {
                fantasy = false;
                HpGet(finalDmg);
            }
            return finalDmg;
        }
    }
}

using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.HiddenStar
{
    internal class Sakata:WifeBase
    {
        public static int sid = 2001;
        public static int weight = 1000;
        public Sakata()
        {
            imgUrl = "https://i.postimg.cc/L6BXPgNk/2.png";
            name = "坂田合欢乃";
            id= sid;
            description = "存在于大人口中的山姥，只要在山中走丢就会被山姥给吃掉。但实际上其本人温和的多，也不会" +
                "像传说中那样被秘密处理掉。相反，还会以某种形式被保护起来。";
            skillTitle[0] = "深山的山姥";
            skillDescription[0] = "居住深山，缺乏情报。遇到强者的时候总想套近乎，受到的所有伤害-10%。";
            skillTitle[1] = "诅咒之雨";
            skillDescription[1] = "消耗30mp，吟唱0。谋杀前的秋雨，消磨了敌方战斗的意志，使敌方攻击力和法术强度" +
                "-5。持续3回合，重复触发叠加效果但不刷新持续时间。";
            skillTitle[2] = "菜刀研磨";
            skillDescription[2] = "消耗30mp，吟唱1x。把刀磨的更锋利，自身攻击力+10。";
            skillTitle[3] = "深山中的谋杀";
            skillDescription[3] = "消耗50mp，吟唱2x。万事俱备，只差邀请别人来做客了。在敌方没有防备的时候下手，造成" +
                "3倍攻击力的伤害。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=30;
        }

        public override bool CanUseThree()
        {
            return currentMp>=50;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=30;
        }
        public override double GetChantTwo()
        {
            return 1;
        }
        public override double GetChantThree()
        {
            return 2;
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            damage = damage * 9 / 10;
            return base.BeingAttack(attacker, damage, type);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            target.AddBuff(new AttackDown(3, 5));
            target.AddBuff(new MagicDown(3, 5));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            currentAttack += 10;
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(50))
            {
                return 0;
            }
            base.SkillThree(target);
            return base.BeingAttack(this,currentAttack*3,DamageType.physical);
        }
    }
}

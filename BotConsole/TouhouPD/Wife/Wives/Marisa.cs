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
        public static int weight = 200;
        public bool usedSpark;
        public int rounds;
        public Marisa()
        {
            usedSpark = false;
            rounds = 0;
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
            skillDescription[3] = "消耗所有mp，吟唱0x。魔理沙的标志性高火力代表。造成法术强度和消耗mp一半之和2倍的" +
                "法术伤害。此外，因为八卦炉越蓄力越强，使用本技能时每过了1个自己回合伤害额外提升10%。该技能每场战斗" +
                "只能使用一次。";
        }

        public override bool CanUseOne()
        {
            return currentMp >= maxMpFinal * 3 / 10;
        }

        public override bool CanUseThree()
        {
            return currentMp >= maxMpFinal / 10;
        }

        public override bool CanUseTwo()
        {
            return !usedSpark;
        }
        public override void RoundStart(WifeBase enemy)
        {
            base.RoundStart(enemy);
            rounds++;
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            string hint = name + "受到了原本伤害为" + damage + "点的";
            BeingAttackEvent(this, attacker);
            if (new Random().Next(100) < currentMissrate)
            {
                damage = 0;
                battleNotice.Add(name + "闪开了攻击。");
                return damage;
            }
            if (new Random().Next(0, 100) < attacker.criticalFinal)
            {
                battleNotice.Add(name + "被暴击了！");
                damage = (int)(damage * attacker.criticalDamage / 100);
            }
            if (isDefending)
                damage = damage / 2;
            int calculateDef = currentDefend, calculateMdef = currentMdefend;
            calculateDef = calculateDef * (100 - attacker.attackPierceRate) / 100;
            calculateMdef = calculateMdef * (100 - attacker.magicPierceRate) / 100;
            calculateMdef -= attacker.magicPierce;
            calculateDef -= attacker.attackPierce;
            switch (type)
            {

                case DamageType.physical:
                    hint += "物理伤害";
                    damage = damage * 100 / (100 + calculateDef < 0 ? 100 : 100 + calculateDef);
                    damage -= currentMp;
                    if (damage < 0)
                        damage = 0;
                    break;
                case DamageType.magic:
                    hint += "魔法伤害";
                    damage = damage * 100 / (100 + calculateMdef < 0 ? 100 : 100 + calculateMdef);
                    break;
                case DamageType.truth:
                    hint += "真实伤害";
                    break;
            }
            battleNotice.Add(hint);
            HpReduce(damage);
            return damage;
        }
        public override double GetChantOne()
        {
            return 0.5;
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(maxMpFinal*3/10))
            {
                return 0;
            }
            target.disarm = 3;
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if (!MpReduce(maxMpFinal / 10))
            {
                return 0;
            }
            var i = new Random().Next(100);
            var damage = currentMagic * (100 + i) / 100;
            return target.BeingAttack(this,damage,DamageType.magic);
        }
        public override int SkillThree(WifeBase target)
        {
            usedSpark = true;
            var extra = currentMp / 2;
            MpReduce(currentMp);
            int damage = (currentMagic + extra) * 2;
            damage *= (10 + rounds) / 10;
            return target.BeingAttack(this,damage,DamageType.magic);
        }
    }
}

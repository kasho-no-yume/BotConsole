using BotConsole.TouhouPD.Buff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives
{
    internal class Yorihime:WifeBase
    {
        public static int weight = 40;
        public static int sid = 1;
        public bool filthyGod;
        public bool gionsama;//祇园神
        public int izunome;//伊豆能卖
        public int amatsumi;//天津瓮星
        public int kanayamahi;//金山彦命
        public int amaterasu;//天照御神
        public int gionsamaCold;//祇园神
        public int kanayamahiCold;//金山彦命
        public int amatsumiCold;//天津瓮星
        public int amaterasuCold;//天照御神
        public Yorihime()
        {
            gionsama = false;
            amaterasu = 0;
            izunome = 0;
            amatsumi = 0;
            gionsamaCold = 0;
            kanayamahiCold = 0;
            amatsumiCold = 0;
            amaterasuCold = 0;
            kanayamahi = 0;
            filthyGod = true;
            id = 1;
            name = "绵月依姬";
            imgUrl = "https://i.postimg.cc/fbwGmZmq/image.png";
            maxHpBase = 300;
            maxMpBase = 20;
            attackBase = 35;
            attackAddition = 2;
            magicAddition = 2;
            speedBase = 8;
            speedAddition = 2;
            defendAddition = 2;
            mdefendAddition = 2;
            hpAddition = 17;
            mpAddition = 2;
            description = "月之暗面的月人。曾经在完全碾压的情况下完胜以红魔馆馆主为首的月球异变团伙。其实力，就" +
                "连幻想乡的大贤者都无法与之正面对抗。";
            skillTitle[0] = "神灵凭依的天上人";
            skillDescription[0] = "坐拥超越八百万神明的力量，依姬几乎无懈可击。其受到的所有伤害会减少当前mp点数。" +
                "若是伤害小于其当前mp则会被直接无视。此外，每使用一种降神仪式，依姬所降神的种类会切换为另一种。" +
                "依姬初始的降神是秽神（写在技能左边的）。";
            skillTitle[1] = "伊邪那岐/伊豆能卖";
            skillDescription[1] = "消耗20%hp/mp，吟唱0.2x。清除敌方所有正面buff/清除自己所有负面buff并且让自己" +
                "两回合内不会被施加负面buff。";
            skillTitle[2] = "祇园之神/天照大神";
            skillDescription[2] = "消耗20%hp/mp，吟唱0.2x。下一次自己受到的非buff伤害减50%，" +
                "且不会因为该次伤害死亡（最多被扣至1血），返还敌方另外50%该伤害。" +
                "/四回合内，回复造成伤害的30%生命值和造成伤害5%的mp。该技能的秽神和崇神各有5回合冷却。";
            skillTitle[3] = "天津瓮星/金山彦命";
            skillDescription[3] = "消耗20%hp/mp，吟唱0。基于敌我双方法强总和，每回合对敌方造成总值20%的法术伤害" +
                "/基于敌我双方攻击力总和，每回合对敌方造成总值20%的物理伤害。此技能重复触发会叠加。" +
                "该技能的秽神和崇神各有5回合冷却。";
        }

        public override void HpReduce(int amount)
        {
            amount -= currentMp;
            amount=amount<=0?0:amount;
            base.HpReduce(amount);
        }
        public override void AddBuff(BuffBase buff)
        {
            if(izunome>0&&buff.effect==BuffBase.Effect.negative)
            {
                return;
            }
            base.AddBuff(buff);
        }
        public override int Attack(WifeBase target)
        {
            int dmg = base.Attack(target);
            if (amaterasu > 0)
            {
                HpGet(dmg * 3 / 10);
                MpGet(dmg / 20);
            }
            return dmg;
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            int finalDmg = currentHp;
            if(gionsama)
            {
                base.BeingAttack(attacker, damage / 2, type);
                attacker.BeingAttack(this, damage / 2, type);
                if(currentHp<=0)
                {
                    currentHp = 1;
                }
            }
            else
            {
                base.BeingAttack(attacker, damage , type);
            }
            finalDmg -= currentHp;
            return finalDmg;
        }
        public override bool CanUseOne()
        {
            if(filthyGod)
            {
                return currentHp >= maxHpFinal / 5;
            }
            return currentMp >= maxMpFinal / 5;
        }

        public override bool CanUseThree()
        {
            if (filthyGod)
            {
                return currentHp >= maxHpFinal / 5;
            }
            return currentMp >= maxMpFinal / 5;
        }

        public override bool CanUseTwo()
        {
            if (filthyGod)
            {
                return currentHp >= maxHpFinal / 5;
            }
            return currentMp >= maxMpFinal / 5;
        }
        public override double GetChantOne()
        {
            return 0.2;
        }
        public override double GetChantTwo()
        {
            return 0.2;
        }
        public override void RoundStart(WifeBase enemy)
        {
            base.RoundStart(enemy);
            izunome--;
            Kanayamahi(enemy);
            Amatsumi(enemy);
            amaterasuCold--;
            gionsamaCold--;
            kanayamahiCold--;
            amatsumiCold--;
        }
        private void Amatsumi(WifeBase enemy)
        {
            int dmg=enemy.BeingAttack(this, amatsumi, DamageType.magic);
            if(amaterasu>0)
            {
                HpGet(dmg * 3 / 10);
                MpGet(dmg / 20);
            }
        }
        private void Kanayamahi(WifeBase enemy)
        {
            int dmg=enemy.BeingAttack(this, kanayamahi, DamageType.physical);
            if (amaterasu > 0)
            {
                HpGet(dmg * 3 / 10);
                MpGet(dmg / 20);
            }
        }
        public override int SkillOne(WifeBase target)
        {
            if(filthyGod && currentHp >= maxHpFinal / 5)
            {
                currentHp -= maxHpFinal / 5;
                filthyGod = !filthyGod;
                for (int i = target.buffList.Count - 1; i >= 0; i--)
                {
                    if (target.buffList[i].effect == BuffBase.Effect.positive)
                    {
                        target.RemoveBuff(target.buffList[i]);
                    }
                }
            }
            else if(!filthyGod&&currentMp>=maxMpFinal/5)
            {
                filthyGod = !filthyGod;
                MpReduce(maxMpFinal / 5);
                for (int i = buffList.Count - 1; i >= 0; i--)
                {
                    if (buffList[i].effect == Buff.BuffBase.Effect.negative)
                    {
                        RemoveBuff(buffList[i]);
                    }
                }
                izunome = 2;
            }
            else
            {
                return 0;
            }
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if (filthyGod && currentHp >= maxHpFinal / 5&&gionsamaCold<=0)
            {
                gionsamaCold = 5;
                currentHp -= maxHpFinal / 5;
                filthyGod = !filthyGod;
                gionsama = true;
            }
            else if (!filthyGod && currentMp >= maxMpFinal / 5&&amaterasuCold<=0)
            {
                filthyGod = !filthyGod;
                MpReduce(maxMpFinal / 5);
                amaterasuCold = 5;
                amaterasu = 4;
            }
            else
            {
                return 0;
            }
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if (filthyGod && currentHp >= maxHpFinal / 5 && amatsumiCold <= 0)
            {
                amatsumiCold = 5;
                currentHp -= maxHpFinal / 5;
                filthyGod = !filthyGod;
                amatsumi += (currentAttack + target.currentAttack) * 3 / 10;
            }
            else if (!filthyGod && currentMp >= maxMpFinal / 5 && kanayamahiCold <= 0)
            {
                filthyGod = !filthyGod;
                kanayamahiCold = 5;
                MpReduce(maxMpFinal / 5);
                kanayamahi += (currentMagic + target.currentMagic) * 3 / 10;
            }
            else
            {
                return 0;
            }
            return base.SkillThree(target);
        }
    }
}

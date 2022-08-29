using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives
{
    internal class Merry:WifeBase
    {
        public static int weight = 5;
        public static int sid=0;
        public bool darknessMoon;
        public Merry()
        {
            darknessMoon = false;
            imgUrl = "https://i.postimg.cc/Vk5LNgtz/2.png";
            id = 0;
            name = "梅莉";
            maxHpBase = 20;
            maxMpBase = 400;
            attackBase = 1;
            magicBase = 30;
            speedBase = 25;
            defendBase = 2;
            mdefendBase = 35;
            missrateBase = 0;
            hpAddition = 1;
            mpAddition = 21;
            attackAddition = 0;
            magicAddition = 3;
            speedAddition = 1;
            defendAddition = 2;
            mdefendAddition = 3;
            description = "科学世纪的超自然少女？比起科学却跟幻想乡有着朦胧的关系。使用着远超普通人类" +
                "的力量，但体质却弱于一般水平。";
            skillTitle[0] = "梦与现实的境界";
            skillDescription[0] = "梅莉受到伤害时，会用mp量1:1抵消伤害，此外，每回合梅莉回复10%mp。" +
                "梅莉的普通攻击基于法术强度造成伤害。若敌方同时拥有‘正粒子’和‘反粒子’，梅莉" +
                "下一次攻击（包括技能）造成的伤害翻倍，且给敌方一次一回合眩晕。触发后，所有‘粒子’" +
                "效果去除。";
            skillTitle[1] = "魅知之旅";
            skillDescription[1] = "消耗30%mp，吟唱1x。梅莉解除自身所有的负面buff，并给敌方添加" +
                "‘正粒子’效果。拥有‘正粒子’效果的敌方会被清除所有正面buff。";
            skillTitle[2] = "月之暗面";
            skillDescription[2] = "消耗30%mp，吟唱1x。梅莉下一次的释放技能不需要吟唱，并给敌方添加" +
                "‘反粒子’效果。拥有‘反粒子’效果的敌方所有属性降低30%";
            skillTitle[3] = "醉生一梦";
            skillDescription[3] = "消耗50%mp，吟唱2x。造成两倍法术攻击的伤害，基于已消耗mp的比例" +
                "的两倍增加本技能的伤害。";
        }

        public override bool CanUseOne()
        {
            if(currentMp>=maxMpFinal*3/10)
            {
                return true;
            }
            return false;
        }
        public override bool CanUseThree()
        {
            if (currentMp >= maxMpFinal / 2)
            {
                return true;
            }
            return false;
        }
        public override bool CanUseTwo()
        {
            if (currentMp >= maxMpFinal *3/10)
            {
                return true;
            }
            return false;
        }
        public override double GetChantOne()
        {
            if(darknessMoon)
            {
                darknessMoon = false;
                return 0;
            }
            return 1;
        }
        public override double GetChantTwo()
        {
            if (darknessMoon)
            {
                darknessMoon = false;
                return 0;
            }
            return 1;
        }
        public override double GetChantThree()
        {
            if (darknessMoon)
            {
                darknessMoon = false;
                return 0;
            }
            return 2;
        }
        public override int Attack(WifeBase target)
        {
            OnAttackEvent(this, target);
            int damage = currentMagic;
            if(PNN(target))
            {
                damage *= 2;
                target.cantActRound = 1;
            }
            return target.BeingAttack(this, damage, DamageType.magic);
        }
        public override void HpReduce(int amount)
        {
            if(invincible>0)
            {
                return;
            }
            if(currentMp>=amount)
            {
                MpReduce(amount);
            }
            else
            {
                amount -= currentMp;
                MpReduce(currentMp);
                base.HpReduce(amount);
            }            
        }
        public override void RoundStart(WifeBase enemy)
        {
            base.RoundStart(enemy);
            MpGet(maxMpFinal / 10);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(maxMpFinal * 3 / 10))
            {
                return 0;
            }
            for(int i=buffList.Count-1;i>=0;i--)
            {
                if (buffList[i].effect==Buff.BuffBase.Effect.negative)
                {
                    RemoveBuff(buffList[i]);
                }
            }
            target.AddBuff(new PositivePart(10000));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(maxMpFinal * 3 / 10))
            {
                return 0;
            }
            darknessMoon = true;
            target.AddBuff(new NegativePart(10000));
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(maxMpFinal / 2))
            {
                return 0;
            }
            base.SkillThree(target);
            double rate = ((maxMpFinal - currentMp) / maxMpFinal*2)+1;
            int damage = currentMagic * 2;
            if(PNN(target))
            {
                damage *= 2;
                target.cantActRound = 1;
            }
            damage = (int)(damage * rate);
            return target.BeingAttack(this,damage,DamageType.magic);
        }
        private bool PNN(WifeBase enemy)
        {
            if(enemy.ExistBuff("正粒子")&&enemy.ExistBuff("反粒子"))
            {
                enemy.RemoveBuff("正粒子");
                enemy.RemoveBuff("反粒子");
                return true;
            }
            return false;
        }
        public override string GetState()
        {
            string res = darknessMoon ? "月之暗面\n" : "";
            res = base.GetState();
            return res;
        }
    }
}

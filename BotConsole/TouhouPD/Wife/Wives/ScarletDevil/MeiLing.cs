using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class MeiLing : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1004;
        public int coldRound;
        public int immuneRound;
        public int totalDmg;
        public MeiLing()
        {
            coldRound = 0;
            immuneRound = -1;
            totalDmg = 0;
            imgUrl = "https://i.postimg.cc/J7GqbDkK/3.png";
            id = 1004;
            name = "红美玲";
            maxHpBase = 250;
            hpAddition = 16;
            attackPierce = 20;
            defendBase = 60;
            description = "虽说是红魔馆的守门人，长得非常像人类但其实还是妖怪。比起绚烂的弹幕，" +
                "似乎更中意使用武术战斗。";
            skillTitle[0] = "红海皇";
            skillDescription[0] = "武术精髓以柔克刚。红美玲自带20点物理穿甲和额外40点物理防御。";
            skillTitle[1] = "彩光莲华掌";
            skillDescription[1] = "消耗0mp，吟唱1x。打出力透纸背的一掌，造成0.8倍攻击力伤害，并且" +
                "降低敌方物理防御10%";
            skillDescription[2] = "消耗100mp，吟唱0x。运转体内的气，将其化为气功炮打出的武林绝学。造成2倍" +
                "攻击力的法术伤害。";
            skillTitle[2] = "破山炮";
            skillDescription[3] = "消耗40%hp，吟唱1x。三回合内无效化受到的所有物理伤害并在结束后以1倍" +
                "总量的物理攻击返还给对手。冷却5回合。";
            skillTitle[3] = "虹色太极拳";
        }

        public override bool CanUseOne()
        {
            return true;
        }

        public override bool CanUseThree()
        {
            return currentHp>=maxHpFinal*2/5&&coldRound<=0;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=100;
        }
        public override double GetChantOne()
        {
            return 1;
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override void RoundStart(WifeBase enemy)
        {
            base.RoundStart(enemy);
            coldRound--;
            immuneRound--;
            if(immuneRound==0)
            {
                enemy.BeingAttack(this, totalDmg, DamageType.physical);
                totalDmg = 0;
            }
        }
        public override int SkillOne(WifeBase target)
        {
            base.SkillOne(target);
            target.currentDefend -= target.defendBase / 10;
            return target.BeingAttack(this,currentAttack*8/10,DamageType.physical);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(100))
            {
                return 0;
            }
            base.SkillOne(target);
            return target.BeingAttack(this,2*currentAttack,DamageType.magic);
        }
        public override int SkillThree(WifeBase target)
        {
            if(currentHp<=maxHpFinal*2/5)
            {
                return 0;
            }
            coldRound = 5;
            immuneRound = 3;
            HpReduce(maxHpFinal*2/5);
            return base.SkillThree(target);
        }
        public override int BeingAttack(WifeBase attacker, int damage, DamageType type)
        {
            if(immuneRound>0&&type==DamageType.physical)
            {
                OnAttackEvent(this, attacker);
                totalDmg += damage;
                return 0;
            }            
            return base.BeingAttack(attacker, damage, type);
        }
    }
}

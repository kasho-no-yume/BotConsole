using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Daiyousei : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1001;
        public int threeCool;
        public int twoSus;
        public Daiyousei()
        {
            threeCool = 0;
            twoSus = -1;
            imgUrl = "https://i.postimg.cc/90jK6mdD/2A.png";
            maxHpBase = 100;
            maxMpBase = 70;
            speedBase = 28;
            speedAddition = 2;
            hpAddition = 12;
            attackBase = 10;
            magicBase = 10;
            defendBase = 10;
            mdefendBase = 10;
            id = 1001;
            name = "大妖精";
            description = "单纯自由的妖精。比起其他普通妖精，有着更快的速度。";
            skillTitle[0] = "自由妖精";
            skillDescription[0] = "作为无忧无虑的妖精，其速度比正常水平略微高亿点。";
            skillTitle[1] = "恶作剧";
            skillDescription[1] = "消耗30mp，吟唱0。偷取敌方随机属性的10%给自己。";
            skillTitle[2] = "捉迷藏";
            skillDescription[2] = "消耗40mp，吟唱0。两回合内，将自己的闪避提升至60。";
            skillTitle[3] = "你弄痛我了";
            skillDescription[3] = "消耗40mp，吟唱1x。基于已损失的生命值的1.25倍，返还敌方法术伤害。冷却一回合。";
        }
        public override string GetState()
        {
            string res = twoSus > 0 ? "捉迷藏持续时间" + twoSus + '\n' : "";
            res += threeCool > 0 ? "三技能冷却时间" + threeCool + '\n' : "";
            return res+base.GetState();
        }
        public override bool CanUseOne()
        {
            return currentMp>=30;
        }
        public override double GetChantThree()
        {
            return 1;
        }
        public override bool CanUseThree()
        {
            return currentMp>=40&&threeCool<=0;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=40;
        }
        public override void RoundStart(WifeBase enemy)
        {
            threeCool--;
            twoSus--;
            if(twoSus==0)
            {
                currentMissrate = missrateBase;
            }
            base.RoundStart(enemy);
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(30))
            {
                return 0;
            }
            int random = new Random().Next(5);
            string iden="";
            int amount = 0;
            switch(random)
            {
                case 0:currentAttack += target.attackBase / 10;
                    target.currentAttack -= target.attackBase / 10;
                    iden = "攻击力";
                    amount= target.attackBase / 10;
                    break;
                case 1:
                    currentMagic += target.magicBase / 10;
                    target.currentMagic -= target.magicBase / 10;
                    iden = "法术强度";
                    amount= target.magicBase / 10;
                    break;
                case 2:
                    currentSpeed += target.speedBase / 10;
                    target.currentSpeed -= target.speedBase / 10;
                    amount = target.speedBase / 10;
                    iden = "速度";
                    break;
                case 3:
                    currentDefend += target.defendBase / 10;
                    target.currentDefend -= target.defendBase / 10;
                    amount = target.defendBase / 10;
                    iden = "防御力";
                    break;
                case 4:
                    currentMdefend += target.mdefendBase / 10;
                    target.currentMdefend -= target.mdefendBase / 10;
                    amount = target.mdefendBase / 10;
                    iden = "法术防御";
                    break;
            }
            battleNotice.Add("大妖精偷取了敌方的"+iden+amount+"点！");
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(40))
            {
                return 0;
            }
            currentMissrate = 60;
            twoSus = 2;
            return base.SkillTwo(target);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(40))
            {
                return 0;
            }
            int damage = (maxHpFinal - currentHp) * 5/4;
            threeCool = 2;
            base.SkillThree(target);
            return target.BeingAttack(this,damage,DamageType.magic);
        }
    }
}

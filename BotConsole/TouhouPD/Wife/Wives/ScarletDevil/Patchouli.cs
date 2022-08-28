using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Patchouli : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1006;
        public bool firstStart;
        public Patchouli()
        {
            firstStart = true;
            imgUrl = "https://i.postimg.cc/c12cTx7W/4-B.png";
            name = "帕秋莉";
            id = 1006;
            description = "红魔馆里的大魔导师，表现上就是个只会看书的宅，运动能力还远弱于常人。但是" +
                "说起魔法，那可是名副其实的移动的图书馆。";
            maxHpBase = 100;
            maxMpBase = 300;
            attackBase = 0;
            defendBase = 0;
            mdefendBase = 40;
            hpAddition = 7;
            mpAddition = 5;
            magicBase = 40;
            magicAddition = 2;
            speedBase = 8;
            skillTitle[0] = "不动的大图书馆";
            skillDescription[0] = "帕秋莉知晓世上的一切魔法。战斗开始时基于敌方法术攻击的一半，增加" +
                "自己的法术攻击。";
            skillTitle[1] = "沉静月神";
            skillDescription[1] = "消耗50mp，吟唱1.5x。沉默敌方2回合。";
            skillTitle[2] = "皇家圣焰";
            skillDescription[2] = "消耗100mp，吟唱0.2x。造成1.5倍魔法攻击力的魔法伤害。";
            skillTitle[3] = "贤者之石";
            skillDescription[3] = "消耗200mp，吟唱3x。造成4倍魔法攻击力的魔法伤害。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=50;
        }

        public override bool CanUseThree()
        {
            return currentMp>=200;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=100;
        }
        public override void RoundStart(WifeBase enemy)
        {
            if(firstStart)
            {
                currentMagic += enemy.magicBase / 2;
                firstStart = false;
            }
            base.RoundStart(enemy);
        }
        public override double GetChantOne()
        {
            return 1.5;
        }
        public override double GetChantTwo()
        {
            return 0.2;
        }
        public override double GetChantThree()
        {
            return 3;
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(50))
            {
                return 0;
            }
            target.silent = 2;
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(100))
            {
                return 0;
            }
            base.SkillTwo(target);
            return target.BeingAttack(this,currentMagic*3/2,DamageType.magic);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(200))
            {
                return 0;
            }
            base.SkillThree(target);
            return target.BeingAttack(this,currentMagic*4,DamageType.magic);
        }
    }
}

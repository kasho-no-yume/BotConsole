using BotConsole.TouhouPD.Buff.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife.Wives.ScarletDevil
{
    internal class Rumia : WifeBase
    {
        public static int weight = 1000;
        public static int sid = 1000;
        public Rumia()
        {
            imgUrl = "https://i.postimg.cc/ZRNXsBdk/1.png";
            missrateBase = 10;
            maxHpBase = 100;
            hpAddition = 9;
            name = "露米娅";
            id = 1000;
            description = "拥有操纵黑暗能力的妖精，但是实力却意外的不强。将自己置身黑暗本来是为了不让" +
                "自己那么显眼，结果反而更显眼了？";
            skillTitle[0] = "霄暗的妖精";
            skillDescription[0] = "露米娅致力于把自己保持黑暗，让敌方看不见自己。自身闪避率提高10。";
            skillTitle[1] = "月亮光";
            skillDescription[1] = "消耗60mp，吟唱1x。增加‘月亮光’效果提高自身40闪避率，持续3回合。" +
                "重复使用刷新持续回合。";
            skillTitle[2] = "夜雀";
            skillDescription[2] = "消耗20mp，吟唱0.5x。造成1倍攻击力的物理伤害，额外提高闪避率比例的伤害。";
            skillTitle[3] = "境界线";
            skillDescription[3] = "消耗40mp，吟唱2x。造成2倍法术强度的法术伤害。额外提高2倍闪避率比例的伤害。";
        }

        public override bool CanUseOne()
        {
            return currentMp>=60;
        }

        public override bool CanUseThree()
        {
            return currentMp >= 40;
        }

        public override bool CanUseTwo()
        {
            return currentMp>=20;
        }
        public override double GetChantOne()
        {
            return 1;
        }
        public override double GetChantTwo()
        {
            return 0.5;
        }
        public override double GetChantThree()
        {
            return 2;
        }
        public override int SkillOne(WifeBase target)
        {
            if(!MpReduce(60))
            {
                return 0;
            }
            AddBuff(new Moonlight(3, 40));
            return base.SkillOne(target);
        }
        public override int SkillTwo(WifeBase target)
        {
            if(!MpReduce(20))
            {
                return 0;
            }
            base.SkillTwo(target);
            int damage = currentAttack * (currentMissrate + 100) / 100;
            return target.BeingAttack(this,damage,DamageType.physical);
        }
        public override int SkillThree(WifeBase target)
        {
            if(!MpReduce(40))
            {
                return 0;
            }
            base.SkillThree(target);
            int damage = currentMagic * (currentMissrate * 2 + 100) / 100;
            return target.BeingAttack(this,damage,DamageType.magic);
        }
    }
}

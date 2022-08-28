using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Equipment
{
    internal abstract class Equip
    {
        public enum Quality
        {
            R,SR,SSR
        }
        public Quality quality;
        public static int sid;
        public int id;
        public string name;
        public string description;
        public int level;
        public int maxLevel;
        public int extraHp;
        public int extraMp;
        public int extraAtk;
        public int extraMagic;
        public int extraSpeed;
        public int extraDef;
        public int extraMdef;
        public int extraAtkPierce;
        public int extraMagicPierce;
        public int extraCritical;
        public int extraCriticalDmg;
        public int extraMissrate;
        public int extraHpRate;
        public int extraMpRate;
        public int extraAtkRate;
        public int extraMagicRate;
        public int extraSpeedRate;
        public int extraDefRate;
        public int extraMdefRate;
        public int extraAtkPierceRate;
        public int extraMagicPierceRate;
        public string spellDescription;
        public Equip()
        {
            maxLevel = 5;
            level = 1;
            spellDescription = "";
        }
        public virtual void Equipping(WifeBase wife)
        {
            wife.currentAttack += extraAtk;
            wife.currentAttack += (wife.attackBase*extraAtkRate/100);
            wife.currentDefend += extraDef;
            wife.currentDefend += (wife.defendBase*extraDefRate/100);
            wife.currentMagic += extraMagic;
            wife.currentMagic += (wife.magicBase * extraMagicRate / 100);
            wife.currentMdefend += extraMdef;
            wife.currentMdefend += (wife.mdefendBase * extraMdefRate / 100);
            wife.currentSpeed += extraSpeed;
            wife.currentSpeed += (wife.speedBase * extraSpeedRate / 100);
            wife.maxMpFinal += extraMp;
            wife.maxHpFinal += extraHp;
            wife.maxMpFinal += (wife.maxMpBase * extraMpRate / 100);
            wife.maxHpFinal += (wife.maxHpBase * extraHpRate / 100);
            wife.currentMp = wife.maxMpFinal;
            wife.currentHp = wife.maxHpFinal;
            wife.currentMissrate += extraMissrate;
            wife.criticalFinal += extraCritical;
            wife.criticalDamage += extraCriticalDmg;
            wife.attackPierce += extraAtkPierce;
            wife.attackPierceRate += extraAtkPierceRate;
            wife.magicPierce += extraMagicPierce;
            wife.magicPierceRate += extraMagicPierceRate;
            Spell(wife);
        }
        public string Introduce()
        {
            string res = "【"+name+"】\n";
            char n = '\n';
            res += description + n;
            res += "【属性】\n";
            res += "品质：" + quality.ToString()+n;
            res += "等级：" + level + n;
            res += extraHp == 0 ? "" : "额外生命+"+extraHp+n;
            res += extraHpRate == 0 ? "" : "额外生命+" + extraHpRate + '%' + n;
            res += extraMp == 0 ? "" : "额外法力值+" + extraMp + n;
            res += extraMpRate == 0 ? "" : "额外法力值+" + extraMpRate + '%' + n;
            res += extraAtk == 0 ? "" : "额外攻击力+" + extraAtk + n;
            res += extraAtkRate == 0 ? "" : "额外攻击力+" + extraAtkRate + '%' + n;
            res += extraMagic == 0 ? "" : "额外法强+" + extraMagic + n;
            res += extraMagicRate == 0 ? "" : "额外法强+" + extraMagicRate + '%' + n;
            res += extraSpeed == 0 ? "" : "额外速度+" + extraSpeed + n;
            res += extraSpeedRate == 0 ? "" : "额外速度+" + extraSpeedRate + '%' + n;
            res += extraCritical == 0 ? "" : "额外暴击率+" + extraCritical + '%'+ n;
            res += extraCriticalDmg == 0 ? "" : "额外暴击伤害+" + extraCriticalDmg + '%' + n;
            res += extraMissrate == 0 ? "" : "额外闪避率+" + extraMissrate + "%\n";
            res += extraDef == 0 ? "" : "额外防御力+" + extraDef + n;
            res += extraDefRate == 0 ? "" : "额外防御力+" + extraDefRate + '%' + n;
            res += extraMdef == 0 ? "" : "额外法术防御+" + extraMdef + n;
            res += extraMdefRate == 0 ? "" : "额外法术防御+" + extraMdefRate + '%' + n;
            res += extraAtkPierce == 0 ? "" : "额外物理穿透+" + extraAtkPierce + n;
            res += extraAtkPierceRate == 0 ? "" : "额外物理穿透+" + extraAtkPierceRate + '%' + n;
            res += extraMagicPierce == 0 ? "" : "额外法术穿透+" + extraMagicPierce + n;
            res += extraMagicPierceRate == 0 ? "" : "额外法术穿透+" + extraMagicPierceRate + '%' + n;
            res += spellDescription.Length == 0 ? "" : "【神器特质】\n" + spellDescription;
            return res;
        }
        public void SetLevel(int level)
        {
            extraHp *= level;
            extraMp *= level;
            extraAtk *= level;
            extraMagic *= level;
            extraSpeed *= level;
            extraDef *= level;
            extraMdef *= level;
            extraAtkPierce *= level;
            extraMagicPierce *= level;
            extraCritical *= level;
            extraCriticalDmg *= level;
            extraHpRate *= level;
            extraMpRate *= level;
            extraAtkRate *= level;
            extraMagicRate *= level;
            extraSpeedRate *= level;
            extraMissrate *= level;
            extraDefRate *= level;
            extraMdefRate *= level;
            extraAtkPierceRate *= level;
            extraMagicPierceRate *= level;
            this.level = level;
    }
        /// <summary>
        /// 空方法，放心重写
        /// </summary>
        /// <param name="wife"></param>
        public virtual void Spell(WifeBase wife)
        {

        }
    }
}

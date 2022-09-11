using BotConsole.TouhouPD.Buff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Wife
{
    internal abstract class WifeBase
    {
        public int id;
        public string name;
        public string description;
        public string imgUrl;
        public string nick;
        public bool isDefending;
        public int maxHpBase;
        public int maxMpBase;
        public int attackBase;
        public int magicBase;
        public int defendBase;
        public int mdefendBase;
        public int speedBase;
        public int missrateBase;
        public int maxHpFinal;
        public int maxMpFinal;
        public int currentHp;
        public int currentMp;
        public int currentAttack;
        public int currentMagic;
        public int currentDefend;
        public int currentMdefend;
        public int currentSpeed;
        public int currentMissrate;
        public int criticalFinal;
        public int level;
        public int maxLevel;
        public int currentExp;
        public int hpAddition;
        public int mpAddition;
        public int attackAddition;
        public int magicAddition;
        public int defendAddition;
        public int mdefendAddition;
        public int speedAddition;
        public int attackPierce;
        public int magicPierce;
        public int attackPierceRate;
        public int magicPierceRate;
        public int criticalBase;
        public int criticalDamage;
        public string[] skillDescription = new string[4];
        public string[] skillTitle=new string[4];
        public BattleNotice battleNotice;
        /// <summary>
        /// 能在几回合内开始行动，建议是在不能行动的时候不处理回合开始方法。
        /// 这个也不建议在wife类内处理，是上层使用的字段。
        /// </summary>
        public int cantActRound;
        public int invincible;
        public int silent;
        public int disarm;
        public List<BuffBase> buffList;
        public enum DamageType
        {
            physical,magic,truth
        }

        public delegate void WifeEvent(WifeBase self,WifeBase? opponent);
        public WifeEvent BeingAttackEvent;
        public WifeEvent OnAttackEvent;
        public WifeEvent OnSkillEvent;
        public WifeEvent OnDeathingEvent;
        public WifeEvent OnRoundStartEvent;
        public delegate void DmgEvent(WifeBase self, int amount);
        public DmgEvent OnHpReduce;
        public delegate void BuffEvent(BuffBase buff);
        public BuffEvent OnBuffAdded;
        public BuffEvent OnBuffRemoved;
        private void NullEvent(WifeBase self, WifeBase? opponent)
        {

        }
        private void NullEvent(BuffBase buff)
        {

        }
        private void NullEvent(WifeBase self,int amount)
        {

        }
        public WifeBase()
        {
            silent = 0;
            cantActRound = 0;
            invincible = 0;
            buffList = new List<BuffBase>();
            isDefending = false;
            //普通老婆的普通属性值标准
            criticalDamage = 150;
            criticalBase = 0;
            attackPierce = 0;
            magicPierce = 0;
            attackPierceRate = 0;
            magicPierceRate = 0;
            maxHpBase = 200;
            maxMpBase = 100;
            attackBase = 20;
            magicBase = 20;
            speedBase = 15;
            defendBase = 20;
            mdefendBase = 20;
            missrateBase = 0;
            hpAddition = 10;
            mpAddition = 2;
            attackAddition = 1;
            magicAddition = 1;
            speedAddition = 1;
            defendAddition = 1;
            mdefendAddition = 1;
            level = 1;
            maxLevel = 100;
            for(int i=0;i<4;i++)
            {
                skillDescription[i] = "无";
                skillTitle[i] = "无";
            }
        }
        /// <summary>
        /// 获得经验
        /// </summary>
        /// <param name="amount">获得经验量</param>
        /// <returns>返回是否因获得经验成功升级</returns>
        public bool GetExp(int amount)
        {
            bool isLevelup = false;
            currentExp += amount;
            while(currentExp>=level*100)
            {
                isLevelup = true;
                currentExp-= level*100;
                if(!LevelUp())
                {
                    break;
                }               
            }
            return isLevelup;
        }       
        public bool LevelUp()
        {
            if(level>=maxLevel)
            {
                return false;
            }
            level++;
            maxHpBase += hpAddition;
            maxMpBase += mpAddition;
            attackBase+=attackAddition;
            magicBase+=magicAddition;
            defendBase+=defendAddition;
            mdefendBase += mdefendAddition;
            speedBase+=speedAddition;
            return true;
        }
        public void SetLevel(int aimlevel)
        {
            if (aimlevel > maxLevel)
                aimlevel = maxLevel;           
            maxHpBase += hpAddition * (aimlevel-level);
            maxMpBase += mpAddition * (aimlevel - level);
            attackBase += attackAddition * (aimlevel - level);
            magicBase += magicAddition * (aimlevel - level);
            defendBase += defendAddition * (aimlevel - level);
            mdefendBase += mdefendAddition * (aimlevel - level);
            speedBase += speedAddition * (aimlevel - level);
            level = aimlevel;
        }

        public virtual void AttributeInit(BattleNotice battleNotice)
        {
            maxHpFinal = maxHpBase;
            maxMpFinal = maxMpBase;
            currentHp = maxHpFinal;
            currentMp = maxMpFinal;
            currentAttack = attackBase;
            currentMagic = magicBase;
            currentDefend = defendBase;
            currentMdefend = mdefendBase;
            currentSpeed = speedBase;
            currentMissrate = missrateBase;
            criticalFinal = criticalBase;
            BeingAttackEvent+=NullEvent;
            OnAttackEvent += NullEvent;
            OnSkillEvent += NullEvent;
            OnDeathingEvent += NullEvent;
            OnRoundStartEvent += NullEvent;
            OnBuffAdded += NullEvent;
            OnBuffRemoved += NullEvent;
            OnHpReduce += NullEvent;
            this.battleNotice = battleNotice;
    }
        /// <summary>
        /// 回合开始时的处理，包括buff效果结算，防御状态移除，回合开始委托事件。
        /// 沉默减少，无敌减少，缴械减少
        /// </summary>
        /// <param name="enemy"></param>
        public virtual void RoundStart(WifeBase enemy)
        {
            silent--;
            invincible--;
            disarm--;
            OnRoundStartEvent(this, enemy);
            this.isDefending = false;
            if(buffList.Count>0)
            for(int i=buffList.Count-1;i>=0;i--)
            {
                if(buffList[i].sustainRound<=0)
                {
                    RemoveBuff(buffList[i]);
                }
                else
                {
                    buffList[i].LastingEffect(this);
                }                
            }
        }
        /// <summary>
        /// 含有攻击委托事件，重写时记得加上委托。
        /// 内置计算好了以攻击力的伤害并攻击了。需要去掉这一点的重写。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual int Attack(WifeBase target)
        {
            OnAttackEvent(this, target);
            int damage = this.currentAttack;
            var finalDmg= target.BeingAttack(this, damage, DamageType.physical);
            if(finalDmg>0)
            {
                battleNotice.Add(name+"的攻击造成了"+finalDmg+"点伤害。");
            }
            return finalDmg;
        }
        /// <summary>
        /// 重写时不用鸡方法时需注意，本方法含有以下要素
        /// 被攻击事件委托，攻击者暴击伤害计算，防御计算，闪避计算
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="damage"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual int BeingAttack(WifeBase attacker,int damage,DamageType type)
        {
            BeingAttackEvent(this,attacker);
            if(new Random().Next(100)<currentMissrate)
            {
                damage = 0;
                battleNotice.Add(name+"闪开了攻击。");
                return damage;
            }
            if(new Random().Next(0,100)<attacker.criticalFinal)
            {
                battleNotice.Add(name+"被暴击了！");
                damage = (int)(damage * attacker.criticalDamage / 100);
            }
            if (isDefending)
                damage = damage /2;
            int calculateDef=currentDefend, calculateMdef=currentMdefend;
            calculateDef = calculateDef * (100- attacker.attackPierceRate) / 100;
            calculateMdef = calculateMdef * (100 - attacker.magicPierceRate) / 100;
            calculateMdef -= attacker.magicPierce;
            calculateDef -= attacker.attackPierce;
            string hint = name + "受到了原本伤害为" + damage + "点的";
            switch (type)
            {
                
                case DamageType.physical:
                    hint += "物理伤害";
                    damage = damage * 100 / (100 + calculateDef < 0 ? 100 : 100+calculateDef);
                    break;
                case DamageType.magic:
                    hint += "魔法伤害";
                    damage = damage * 100 / (100 + calculateMdef < 0 ? 100 : 100+calculateMdef);
                    break;
                case DamageType.truth:
                    hint += "真实伤害";
                    break;
            }
            battleNotice.Add(hint);
            HpReduce(damage);
            return damage;
        }
        public virtual bool ExistBuff(string buffName)
        {
            bool res = false;
            foreach(var i in buffList)
            {
                if(i.name.Equals(buffName))
                {
                    return true;
                }
            }
            return res;
        }
        public virtual bool ExistBuff(BuffBase buff)
        {
            return ExistBuff(buff.name);
        }
        /// <summary>
        /// 带有事件委托，重写时需注意。
        /// </summary>
        /// <param name="buff"></param>
        public virtual void AddBuff(BuffBase buff)
        {
            OnBuffAdded(buff);
            if(buff.sustainRound==0)
            {
                return;
            }
            battleNotice.Add(name + "被施加了" + buff.name);
            if(!ExistBuff(buff))
            {
                buff.BeingAdded(this);
                buffList.Add(buff);
            }
            else
            {
                foreach(var i in buffList)
                {
                    if(i.name==buff.name)
                    {
                        i.Repeat(buff);
                    }
                }
            }
        }
        /// <summary>
        /// 带有事件委托，重写时需注意。
        /// </summary>
        /// <param name="buffName"></param>
        public virtual void RemoveBuff(string buffName)
        {
            foreach (var i in buffList)
            {
                if (i.name.Equals(buffName))
                {
                    battleNotice.Add(name+"的"+buffName+"效果被移除。");
                    OnBuffRemoved(i);
                    i.BeingRemoved(this);
                    buffList.Remove(i);
                    return;
                }
            }
        }
        /// <summary>
        /// 确保传入的buff存在，一般是配合大批量移除buff的循环使用。
        /// 带有事件委托，重写时需注意。
        /// </summary>
        /// <param name="buff"></param>
        public virtual void RemoveBuff(BuffBase buff)
        {
            OnBuffRemoved(buff);
            buff.BeingRemoved(this);
            buffList.Remove(buff);
            battleNotice.Add(name + "的" + buff.name + "效果被移除。");
        }
        /// <summary>
        /// hp减少，最底层的hp操作代码
        /// 重写需注意，有无敌代码在里面。
        /// </summary>
        /// <param name="amount"></param>
        public virtual void HpReduce(int amount)
        {
            OnHpReduce(this, amount);
            if(invincible>0)
            {
                return;
            }
            currentHp -= amount;
            battleNotice.Add(name + "的生命值损失了"+amount);
            if (currentHp < 0)
            {
                currentHp = 0;
                OnDeathingEvent(this,null);
            }
        }
        public virtual bool MpReduce(int amount)
        {
            if(currentMp<amount)
            {
                return false;
            }
            currentMp -= amount;
            battleNotice.Add(name + "的法力值损失了" + amount);
            return true;
        }
        public virtual void HpGet(int amount)
        {
            currentHp += amount;
            battleNotice.Add(name + "的生命值回复了" + amount);
            if (currentHp>=maxHpFinal)
            {
                currentHp = maxHpFinal;
            }
        }
        public virtual void MpGet(int amount)
        {
            currentMp += amount;
            battleNotice.Add(name + "的法力值回复了" + amount);
            if (currentMp >= maxMpFinal)
            {
                currentMp = maxMpFinal;
            }
        }
        public virtual void Defend()
        {
            this.isDefending = true;
            MpGet(maxMpFinal/10);
        }
        /// <summary>
        /// 鉴于可能有不同的消耗类型，因此扣cost的代码写在这里面。
        /// 此外，考虑到可能吟唱完的时候就不符合发动条件了，也要取消发动，返回0
        /// </summary>
        /// <param name="target"></param>
        /// <returns>返回对敌方造成的伤害。如果没有伤害则返回0</returns>
        public virtual int SkillOne(WifeBase target)
        {
            OnSkillEvent(this, target);
            return 0;
        }
        public virtual int SkillTwo(WifeBase target)
        {
            OnSkillEvent(this, target);
            return 0;
        }
        public virtual int SkillThree(WifeBase target)
        {
            OnSkillEvent(this, target);
            return 0;
        }
        /// <summary>
        /// 为何不用返回消耗的形式，主要还是考量到会有不同的消耗形式。
        /// </summary>
        /// <returns>能否满足释放对应技能的条件。</returns>
        public abstract bool CanUseOne();
        public abstract bool CanUseTwo();
        public abstract bool CanUseThree();
        /// <summary>
        /// 返回吟唱时间倍数。为何不用数组形式主要还是为了修改吟唱时间的考量。
        /// </summary>
        /// <returns>吟唱时间倍数</returns>
        public virtual double GetChantOne()
        {
            return 0;
        }
        public virtual double GetChantTwo()
        {
            return 0;
        }
        public virtual double GetChantThree()
        {
            return 0;
        }
        public bool CanUseSkill(int id)
        {
            switch(id)
            {
                case 0:if (disarm > 0) return false;
                    return true;
                case 1:if (silent>0) return false;
                    return CanUseOne();
                case 2:
                    if(silent > 0) return false;
                    return CanUseTwo();
                case 3:
                    if(silent > 0) return false;
                    return CanUseThree();
            }
            return false;
        }
        public virtual string GetState()
        {
            string resstr = "";
            resstr += silent > 0 ? "沉默剩余" + silent + '\n' : "";
            resstr += invincible > 0 ? "无敌剩余" + invincible + '\n' : "";
            resstr += disarm > 0 ? "缴械剩余" + disarm + '\n' : "";
            resstr += cantActRound > 0 ? "不能行动剩余" + cantActRound + '\n' : "";
            foreach (var i in buffList)
            {
                resstr += i.name + i.strength + "持续时间：" + i.sustainRound + '\n';
            }
            return resstr;
        }
    }
}

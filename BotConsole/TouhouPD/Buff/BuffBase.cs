using BotConsole.TouhouPD.Wife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Buff
{
    internal abstract class BuffBase
    {
        public enum Effect
        {
            positive,negative
        }
        public int sustainRound;
        public string name;
        public WifeBase owner;
        public Effect effect;
        public int strength;
        /// <summary>
        /// 鸡方法中是给owner赋值，没特殊需要不必删除调用鸡方法。
        /// </summary>
        /// <param name="wife"></param>
        public virtual void BeingAdded(WifeBase wife)
        {
            owner = wife;
        }
        /// <summary>
        /// 鸡方法中是减少持续时间，没特殊需要不必删除调用鸡方法。
        /// </summary>
        /// <param name="wife"></param>
        public virtual void LastingEffect(WifeBase wife)
        {
            sustainRound--;
        }
        public virtual void BeingRemoved(WifeBase wife)
        {

        }
        /// <summary>
        /// 在构造函数中，重写时确立名字，以及效果正负性。
        /// </summary>
        /// <param name="sustainRound"></param>
        public BuffBase(int sustainRound)
        {
            this.sustainRound = sustainRound;            
            strength = 0;
        }
        /// <summary>
        /// 在构造函数中，重写时确立名字，以及效果正负性。
        /// </summary>
        /// <param name="sustainRound"></param>
        /// <param name="strength"></param>
        public BuffBase(int sustainRound,int strength)
        {
            this.sustainRound = sustainRound;
            this.strength = strength;
        }
        /// <summary>
        /// 默认是刷新持续时间，需要改的则重写。
        /// </summary>
        /// <param name="buff"></param>
        public virtual void Repeat(BuffBase buff)
        {
            this.sustainRound = buff.sustainRound;
        }
    }
}

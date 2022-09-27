using BotConsole.TouhouPD.Wife;
using BotConsole.TouhouPD.Wife.Wives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD.Gamer.BotStrategy 
{
    internal class MerryStra : Strategy
    {
        private Merry merry;
        private bool canAttack;
        private int onecool;
        private int twocool;
        private int attacktime;
        public MerryStra(WifeBase wife, WifeBase? enemy) : base(wife, enemy)
        {
            merry = (Merry)wife;
            canAttack = false;
            onecool = 0;
            twocool = 0;
            attacktime = 0;
        }

        public override string HowToDo()
        {
            onecool--;
            twocool--;
            if(merry.currentMp<=merry.maxMpFinal/3)
            {
                return "defend";
            }
            if(UseEx())
            {
                return "skill3";
            }
            if(Attack())
            {
                return "attack";
            }
            canAttack = true;
            if (!enemy.ExistBuff("正粒子") && merry.CanUseSkill(2) && onecool < 0)
            {
                onecool = 3;
                return "skill1";
            }
            if (!enemy.ExistBuff("反粒子")&&merry.CanUseSkill(2)&&twocool<0)
            {
                twocool = 3;
                return "skill2";
            }
            return "defend";
        }
        private bool UseEx()
        {
            if(!merry.CanUseSkill(3))
            {
                return false;
            }
            if(CanDefeat()&&merry.darknessMoon)
            {
                return true;
            }
            if(merry.PNN(enemy)&&merry.darknessMoon)
            {
                return true;
            }
            return false;
        }
        private bool CanDefeat()
        {
            int mp = merry.currentMp - (merry.maxMpFinal / 2);
            if(mp < 0)
            {
                return false;
            }
            double rate = ((merry.maxMpFinal - mp) / (double)merry.maxMpFinal * 2) + 1;
            int damage = merry.currentMagic * 2;
            if (merry.PNN(enemy))
            {
                damage *= 2;
            }
            damage = (int)(damage * rate);
            int mdef = enemy.currentMdefend;
            mdef = mdef * (100 - merry.magicPierceRate) / 100;
            damage = damage * 100 / (100 + mdef);
            if (enemy.isDefending)
            {
                damage /= 2;
            }
            if (damage >= enemy.currentHp)
            {
                return true;
            }
            return false;
        }
        private bool Attack()
        {
            if(!merry.CanUseSkill(0))
            {
                return false;
            }
            if(canAttack)
            {
                canAttack = false;
                return true;
            }
            return false;
        }
    }
}

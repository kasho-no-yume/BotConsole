using BotConsole.TouhouPD.Equipment.SSRare;
using BotConsole.TouhouPD.Equipment.SRare;
using BotConsole.TouhouPD.Equipment.Rare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotConsole.TouhouPD.Equipment.Equip;

namespace BotConsole.TouhouPD.Equipment
{
    internal class EquipFactory
    {
        public static Dictionary<int,Type>  Rare = new Dictionary<int,Type>();
        public static Dictionary<int, Type> SRare = new Dictionary<int, Type>();
        public static Dictionary<int, Type> SSRare = new Dictionary<int, Type>();
        static EquipFactory()
        {
            SSRare.Add(UnderworldMirror.sid, typeof(UnderworldMirror));
            SSRare.Add(KontamaPotion.sid, typeof(KontamaPotion));
            SSRare.Add(Gungnir.sid, typeof(Gungnir));
            SSRare.Add(Laevatain.sid, typeof(Laevatain));
            SRare.Add(UselessSpell.sid,typeof(UselessSpell));
            SRare.Add(MadoBook.sid, typeof(MadoBook));
            SRare.Add(BrokenCrystal.sid,typeof(BrokenCrystal));
            SRare.Add(ExplodeSpell.sid, typeof(ExplodeSpell));
            SRare.Add(TurtleShell.sid, typeof(TurtleShell));
            SRare.Add(LeatherClothes.sid, typeof(LeatherClothes));
            SRare.Add(SilverKnife.sid, typeof(SilverKnife));
            Rare.Add(Stick.sid, typeof(Stick));
            Rare.Add(WoodenShield.sid,typeof(WoodenShield));
            Rare.Add(Yubi.sid, typeof(Yubi));
            Rare.Add(SpiritClothes.sid,typeof(SpiritClothes));
            Rare.Add(LuckyRabbit.sid, typeof(LuckyRabbit));
            Rare.Add(MouseFur.sid, typeof(MouseFur));
            Rare.Add(MagicPotion.sid, typeof(MagicPotion));
            Rare.Add(WolfTooth.sid, typeof(WolfTooth));
            Rare.Add(Duster.sid, typeof(Duster));
            Rare.Add(Sand.sid, typeof(Sand));
        }
        /// <summary>
        /// 返回指定品质下的随机装备sid
        /// </summary>
        /// <param name="quality"></param>
        /// <returns>注意返回的是sid</returns>
        public static int RandomQuality(Quality quality)
        {
            int res = -1;
            switch(quality)
            {
                case Quality.R:
                    res = Rare.Keys.ElementAt(new Random().Next(0, Rare.Count));
                    break;
                case Quality.SR:
                    res = SRare.Keys.ElementAt(new Random().Next(0, SRare.Count));
                    break;
                case Quality.SSR:
                    res = SSRare.Keys.ElementAt(new Random().Next(0, SSRare.Count));
                    break;
            }
            return res;
        }
        public static Equip? GenerateEquip(int sid,int level,int id)
        {
            Equip? res = null;
            Dictionary<int, Type> temp=null;
            if (Rare.ContainsKey(sid)) temp = Rare;
            if (SRare.ContainsKey(sid)) temp = SRare;
            if (SSRare.ContainsKey(sid)) temp = SSRare;
            if(level <= 0||level>=6)
            {
                return res;
            }
            if(temp != null)
            {
                temp.TryGetValue(sid, out var type);
                res = (Equip?)System.Activator.CreateInstance(type);
                res.SetLevel(level);
                res.id = id;
            }
            else if(sid==104)
            {
                res = (Equip?)System.Activator.CreateInstance(typeof(HappyBean));
                res.SetLevel(level);
                res.id = id;
            }
            return res;
        }
    }
}

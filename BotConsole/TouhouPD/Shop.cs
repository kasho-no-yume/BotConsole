using BotConsole.TouhouPD.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    internal static class Shop
    {
        public static int[] equipList = new int[7]; 
        public static int[] equipLevel= new int[7];
        public static int[] equipPrice= new int[7];
        public static Equip[] equips = new Equip[7];
        public static DateTime today;
        static Shop()
        {
            today= DateTime.Now;
            Refresh();
        }
        public static void ShowList(User user)
        {
            if(today.Day!=DateTime.Now.Day)
            {
                Refresh();
            }
            string res = "";
            for(int i=0;i<7;i++)
            {
                if (equipLevel[i]!=0)
                {
                    res += i + " " + equips[i].name + " " + equips[i].quality.ToString() 
                        +" 等级：" + equipLevel[i] + " 价格：" + equipPrice[i]+'\n';
                }             
            }
            res += "输入【购买 序号】购买对应商品。\n";
            res += "输入【查看商品 序号】查看商品信息。\n";
            res += "输入【刷新商品】可以花3000円刷新商品";
            new Sender().QuicklyReply(user.group, res);
        }
        public static void CheckGoods(User user,string param)
        {
            string[] para = param.Split(" ");
            if(para.Length<2)
            {
                return;
            }
            int id;
            if (!int.TryParse(para[1],out id))
            {
                return;
            }
            if (equipLevel[id]==0)
            {
                return;
            }
            string res = equips[id].Introduce();
            new Sender().QuicklyReply(user.group, res);
        }
        public static void Purchase(User user,string param)
        {
            string[] para = param.Split(" ");
            if (para.Length < 2)
            {
                return;
            }
            int id;
            if (!int.TryParse(para[1], out id))
            {
                return;
            }
            if (equipLevel[id] == 0)
            {
                return;
            }
            if (user.CostMoney(equipPrice[id]))
            {
                user.AddEquip(equipList[id], equipLevel[id]);
                new Sender().QuicklyReply(user.group, "购买成功！");
            }
        }
        public static void RefreshGoods(User user)
        {
            if(user.CostMoney(3000))
            {
                Refresh();
                new Sender().QuicklyReply(user.group, "刷新成功");
            }
        }
        private static void Refresh()
        {
            for (int i = 0; i < 4; i++)
            {
                equipList[i] = EquipFactory.RandomQuality(Equip.Quality.R);
                equipLevel[i] = new Random().Next(1, 5);
                equipPrice[i] = 500 * (equipLevel[i] * equipLevel[i]);
            }
            for (int i = 4; i < 6; i++)
            {
                equipList[i] = EquipFactory.RandomQuality(Equip.Quality.SR);
                equipLevel[i] = new Random().Next(1, 5);
                equipPrice[i] = 8000 * (equipLevel[i] * equipLevel[i]);
            }
            if (new Random().Next(100) >= 80)
            {
                equipList[6] = EquipFactory.RandomQuality(Equip.Quality.SSR);
                equipLevel[6] = 1;
                equipPrice[6] = 800000;
            }
            for (int i = 0; i < 7; i++)
            {
                if (equipLevel[i] != 0)
                {
                    equips[i] = EquipFactory.GenerateEquip(equipList[i], equipLevel[i], 0);
                }
            }
        }
    }
}

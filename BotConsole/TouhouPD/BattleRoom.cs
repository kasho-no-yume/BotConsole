using BotConsole.TouhouPD.Equipment;
using BotConsole.TouhouPD.Gamer;
using BotConsole.TouhouPD.Wife;
using BotConsole.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.TouhouPD
{
    /// <summary>
    /// 从该类计算老婆的战斗逻辑。
    /// </summary>
    internal class BattleRoom
    {
        GamePlayer initiator;
        Participant receipent;
        WifeBase one, two;
        string group;
        //吟唱中的技能序号，以及吟唱时间倍率。
        Dictionary<WifeBase, int> skillCache = new Dictionary<WifeBase, int>();
        Dictionary<WifeBase,double>speedRate=new Dictionary<WifeBase,double>();
        List<ForwardMsg> forwardMsgs;
        private BattleNotice battleNotice;
        public BattleRoom(GamePlayer initiator,Participant receipent)
        {
            this.receipent = receipent;
            this.initiator = initiator;
            forwardMsgs = new List<ForwardMsg>();
            one = initiator.wife;
            two = receipent.wife;
            group = initiator.user.group;
            battleNotice = new BattleNotice(group);
        }
        public void GameStart()
        {          
            one.AttributeInit(battleNotice);
            two.AttributeInit(battleNotice);
            if(initiator.weapon!=null)
            {
                initiator.weapon.Equipping(one);
            }
            if(receipent.weapon!=null)
            {
                receipent.weapon.Equipping(two);
            }
            new Sender().QuicklyReply(group, "战斗开始！由" + initiator.name + "对" + receipent.name + "发起的挑战！");
            var thread = new Thread(GameLoop);
            thread.Start();
        }
        private void GameLoop()
        {
            int fullProgress = one.currentSpeed * two.currentSpeed;
            int progressOne=fullProgress,progressTwo=fullProgress;
            skillCache[one] = 0;
            skillCache[two] = 0;
            speedRate[one] = 1;
            speedRate[two] = 1;
            while(one.currentHp>0&&two.currentHp>0)
            {
                progressOne -= (int)(one.currentSpeed / speedRate[one]);
                progressTwo -= (int)(two.currentSpeed / speedRate[two]);
                if(progressOne<=0)
                {
                    if(one.cantActRound<=0)
                    {
                        ProcessAct(one, two, initiator);
                    }
                    else
                    {
                        battleNotice.Add(one.name+"被眩晕，跳过该回合！");
                    }
                    one.cantActRound--;
                    progressOne += fullProgress;
                }
                if(one.currentHp <= 0 || two.currentHp <= 0)
                {
                    break;
                }
                if (progressTwo <= 0)
                {
                    if (two.cantActRound <= 0)
                    {
                        ProcessAct(two, one, receipent);
                    }
                    else
                    {
                        battleNotice.Add(two.name + "被眩晕，跳过该回合！");
                    }
                    two.cantActRound--;
                    progressTwo += fullProgress;
                }
            }
            battleNotice.SendNotice();
            if (initiator.user != null)
            {
                PlayingQQ.PlayingOver(initiator.user.qq);
            }
            if(receipent.user!=null)
            {
                PlayingQQ.PlayingOver(receipent.user.qq);
            }
            if(one.currentHp <= 0 && two.currentHp <= 0)
            {
                new Sender().QuicklyReply(group, "千年奇观，竟然平局了。");
                return;
            }
            User? winner=null;
            Participant loser=receipent;
            if(one.currentHp<=0)
            {
                new Sender().QuicklyReply(group, initiator.name + "输了。");
                winner = receipent.user;
                loser = initiator;
            }
            if(two.currentHp<=0)
            {
                new Sender().QuicklyReply(group,receipent.name+"输了。");
                winner = initiator.user;
                loser = receipent;
            }
            if(winner!=null)
            {
                string bounshint = "";
                foreach(var i in loser.bouns)
                {
                    if(i.Key=="money")
                    {
                        winner.GetMoney(i.Value);
                        bounshint += "你获得了" + i.Value + "円！\n";

                    }
                    if(i.Key=="exp")
                    {
                        winner.ExpConfront(i.Value);
                        bounshint += "你获得了" + i.Value + "经验！\n";
                    }
                    if(i.Key=="equip")
                    {
                        winner.AddEquip(i.Value, 1);
                        bounshint += "你获得了装备" + EquipFactory.GenerateEquip(i.Value, 1, 0).name + "！\n";
                    }
                }
                if(bounshint.Length!=0)
                {
                    new Sender().QuicklyReply(group, bounshint);
                }
            }
        }
        private void ProcessAct(WifeBase self,WifeBase opponent,Participant part)
        {            
            bool ok = false;
            if(self.currentHp<=0||opponent.currentHp<=0)
            {
                return;
            }
            if (skillCache[self]!=0)
            {
                int damage = 0;
                if(self.silent>0)
                {
                    new Sender().QuicklyReply(group, "因为被沉默，所以"+initiator.name+"吟唱的技能没有发动。");
                    skillCache[self] = 0;
                    speedRate[self] = 1;
                    return;
                }
                var res = part.name + "吟唱的技能" + skillCache[self] + "成功发动！\n";
                battleNotice.Add(res);
                switch (skillCache[self])
                {
                    case 1: damage = self.SkillOne(opponent); break;
                    case 2: damage = self.SkillTwo(opponent); break;
                    case 3: damage = self.SkillThree(opponent); break;
                }
                if (part.iden == "player")
                    battleNotice.SendNotice();
                skillCache[self] = 0;
                speedRate[self] = 1;               
                return;
            }
            self.RoundStart(opponent);
            string hint = "现在轮到" + part.name + "行动\n";
            if(part.iden=="player")
            {
                hint += "【己方老婆】" + self.name + " lv." + self.level + '\n';
                hint += String.Format("生命：{0}/{7}\n法力值：{1}/{8}\n攻击力：{2}\n" +
                    "法强：{3}\n速度：{4}\n防御力：{5}\n" + "法术防御：{6}\n"
                    , self.currentHp, self.currentMp, self.currentAttack, self.currentMagic,
                    self.currentSpeed, self.currentDefend, self.currentMdefend, self.maxHpFinal, self.maxMpFinal);
                hint += "【敌方老婆】" + opponent.name + " lv." + opponent.level + '\n';
                hint += String.Format("生命：{0}/{7}\n法力值：{1}/{8}\n攻击力：{2}\n法强：{3}" +
                    "\n速度：{4}\n防御力：{5}\n" + "法术防御：{6}\n"
                    , opponent.currentHp, opponent.currentMp, opponent.currentAttack,
                    opponent.currentMagic, opponent.currentSpeed, opponent.currentDefend, opponent.currentMdefend,
                    opponent.maxHpFinal, opponent.maxMpFinal);
                hint += "——选择你的操作——\n";
                hint += "[攻击]攻击敌方\n[防御]进行防御\n[查看技能]忘记技能效果了？\n[技能123]释放对应技能\n" +
                    "[详细]查看双方详细属性\n[状态]查看敌我buff\n[认输]你就是个loser";
            }          
            battleNotice.Add(hint);
            if (part.iden == "player")
                battleNotice.SendNotice();
            while (!ok)
            {
                switch (part.RequireAct())
                {
                    case "attack":
                        if (self.CanUseSkill(0))
                        {
                            int dmg = self.Attack(opponent);
                            ok = true;
                        }
                        else
                        {
                            new Sender().QuicklyReply(group, "目前不能攻击。");
                        }
                        break;
                    case "skill":
                            string res = "";
                            for(int i=0;i<4;i++)
                            {
                                res += "【"+self.skillTitle[i] + "】:" + self.skillDescription[i] + "\n";
                            }
                            new Sender().QuicklyReply(initiator.user.group, res);
                        break;
                    case "skill1":
                        if(self.CanUseSkill(1))
                        {
                            if(self.GetChantOne()==0)
                            {
                                int dmg=self.SkillOne(opponent);
                                battleNotice.Add(part.name+"释放技能1！造成了" + dmg + "伤害。");
                            }
                            else
                            {
                                skillCache[self] = 1;
                                speedRate[self] = self.GetChantOne();
                                battleNotice.Add(part.name + "开始吟唱技能1");
                            }
                            ok = true;
                        }
                        else
                        {
                            new Sender().QuicklyReply(initiator.user.group, "目前不能使用该技能。");
                        }
                        break;
                    case "skill2":
                        if (self.CanUseSkill(2))
                        {
                            if (self.GetChantTwo() == 0)
                            {
                                int dmg=self.SkillTwo(opponent);
                                battleNotice.Add(part.name + "释放技能2！造成了" + dmg + "伤害。");
                            }
                            else
                            {
                                skillCache[self] = 2;
                                speedRate[self] = self.GetChantTwo();
                                battleNotice.Add(part.name + "开始吟唱技能2");
                            }
                            ok = true;
                        }
                        else
                        {
                            new Sender().QuicklyReply(initiator.user.group, "目前不能使用该技能。");
                        }
                        break;
                    case "skill3":
                        if (self.CanUseSkill(3))
                        {
                            if (self.GetChantThree() == 0)
                            {
                                int dmg=self.SkillThree(opponent);
                                battleNotice.Add(part.name + "释放技能3！造成了" + dmg + "伤害。");
                            }
                            else
                            {
                                skillCache[self] = 3;
                                speedRate[self] = self.GetChantThree();
                                battleNotice.Add(part.name + "开始吟唱技能3");
                            }
                            ok = true;
                        }
                        else
                        {
                            new Sender().QuicklyReply(initiator.user.group, "目前不能使用该技能。");
                        }
                        break;
                    case "defend":
                        self.Defend();
                        ok = true;
                        battleNotice.Add(part.name + "选择了防御。");
                        break;
                    case "state":
                        string resstr = "【己方状态】\n";
                        resstr += self.GetState();
                        resstr += "【敌方状态】\n";
                        resstr += opponent.GetState();
                        new Sender().QuicklyReply(group, resstr);
                        break;
                    case "detail":
                        string detail = "";
                        detail += "【己方老婆】" + self.name + " lv." + self.level + '\n';
                        detail += String.Format("生命：{0}/{7}\n法力值：{1}/{8}\n攻击力：{2}\n" +
                            "法强：{3}\n速度：{4}\n防御力：{5}\n" + "法术防御：{6}\n暴击率：{9}\n暴击伤害{10}%\n" +
                            "闪避率：{11}\n物理穿透：{12}\n物理穿透率：{13}\n法术穿透：{14}\n法术穿透率：{15}"
                            , self.currentHp, self.currentMp, self.currentAttack, self.currentMagic,
                            self.currentSpeed, self.currentDefend, self.currentMdefend, self.maxHpFinal
                            , self.maxMpFinal,self.criticalFinal,self.criticalDamage,self.currentMissrate,
                            self.attackPierce,self.attackPierceRate,self.magicPierce,self.magicPierceRate);
                        detail += "【敌方老婆】" + opponent.name + " lv." + opponent.level + '\n';
                        detail += String.Format("生命：{0}/{7}\n法力值：{1}/{8}\n攻击力：{2}\n法强：{3}" +
                            "\n速度：{4}\n防御力：{5}\n" + "法术防御：{6}\n暴击率：{9}\n暴击伤害{10}%\n" +
                            "闪避率：{11}\n物理穿透：{12}\n物理穿透率：{13}\n法术穿透：{14}\n法术穿透率：{15}"
                            , opponent.currentHp, opponent.currentMp, opponent.currentAttack,
                            opponent.currentMagic, opponent.currentSpeed, opponent.currentDefend, opponent.currentMdefend,
                            opponent.maxHpFinal, opponent.maxMpFinal,opponent.criticalFinal,
                            opponent.criticalDamage, opponent.currentMissrate,
                            opponent.attackPierce, opponent.attackPierceRate, opponent.magicPierce, opponent.magicPierceRate);
                        ForwardMsg forwardMsg = new ForwardMsg(part.name, initiator.user.qq, detail);
                        new Sender().QuicklySendForward(group, forwardMsg);
                        break;
                    case "surrender":
                        ok=true;
                        new Sender().QuicklyReply(group,part.name+"家里的柜子动了，所以先溜了。绝对不是因为打不过才" +
                            "投降的。");
                        self.currentHp = 0;
                        break;
                    case "overtime":
                        ok = true;
                        new Sender().QuicklyReply(group,part.name+"超时了，思考虽好，可不要太久哦。");
                        self.currentHp = 0;
                        break;
                }
                if(part.iden=="player")
                battleNotice.SendNotice();
            }
        }
    }
}

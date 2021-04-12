using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Buffs;
using Zamargrad.Logic.Generators;
using Zamargrad.Logic.Items;
using Zamargrad.Logic.Mobs;
using Zamargrad.Logic.Skills;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Character
{
    public class Person
    {
        public enum classType
        {
            Нету,
            Маг,
            Воин,
            Ассасин,
            Лучник,
        }
        public string id, name, p_Titul, lastRoom;
        public int age,
            money,
            p_Exp, p_Level,
            p_Attack, m_Attack, s_Attack,
            p_Defence, m_Defence, s_Defence,
            m_Agility, m_Strength, m_Intelegence,
            p_Agility, p_Strength, p_Intelegence,
            s_Agility, s_Strength, s_Intelegence,
            p_Exp_Need;
        public float p_Hp, p_MaxHP, p_Mana, p_MaxMana, p_Regen;
        public bool ghost;
        public classType p_Class;

        public int way;
        public bool fight;

        private long myTime;

        public Dictionary<string, Item> items = new Dictionary<string, Item>();
        public Dictionary<string, Skill> skills = new Dictionary<string, Skill>();
        public List<Buff> buffs = new List<Buff>();

        public Person(string id, string name)
        {
            this.id = id;
            this.name = name;

            way = -1;

            p_Hp = 100;
            p_Mana = 100;

            p_Level = 1;
            p_Exp_Need = 20;

            p_Class = 0;
            p_Agility = 0;
            p_Intelegence = 0;
            p_Strength = 0;

            m_Defence = 3;
            m_Attack = 10;

            p_Regen = 20f;

            p_Titul = "Нету";

            myTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            RecalkStat();

        }

        public async Task GetExp(int exp)
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            p_Exp += exp;

            if (p_Exp > p_Exp_Need)
            {
                int piceExp = p_Exp - p_Exp_Need;
                p_Exp = 0;
                p_Level++;
                p_Attack += 5;
                p_Defence += 2;
                p_Exp_Need = 20 * p_Level;

                await Loging.Log(name + " теперь " + p_Level + " уровня!", chanel);

                await ApplyStat(0, 0, 1, 1, 1);
                await GetExp(piceExp);

                return;
            }

            await Loging.Log(name + " получил " + exp + " опыта!", chanel);

        }

        public async Task SetClass (int Agility, int Strenght, int Intelegence){

           await Task.Run(()=>{

            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;


            s_Agility = Agility;
            s_Intelegence = Intelegence;
            s_Strength = Strenght;

            RecalkStat();
            
           });

        }


        //получаем урон
        public async Task GetDamage(float damage)
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            Random rand = new Random();

            damage = rand.Next((int)damage - 15, (int)damage + 15);

            damage = damage - (m_Defence * 0.85f);

            if (damage < 0) damage = 1;

            p_Hp = (int)(p_Hp - damage);
            //Если нет здоровья
            if (p_Hp < 0)
            {
                var mob = TravelService.enemys[way];
                TravelService.lockWay[way] = false;

                mob.heroes.Remove(name);

                way = -1;

                ghost = true;

                fight = false;

                await Loging.Log("Ты проиграл битву и теперь ты дух. Тебе доступен только потусторонний мир!", chanel);

                await TravelService.CheckEnemys();

                return;
            }

            await Loging.Log(name + " получил " + damage + " урона. Осталось " + p_Hp + " здоровья !", chanel);

        }

        public async Task SmashPly(string msg)
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            var mob = TravelService.enemys[way];
            TravelService.lockWay[way] = false;

            mob.heroes.Remove(name);

            way = -1;

            ghost = true;

            fight = false;

            await chanel.SendMessageAsync(msg);

            await TravelService.CheckEnemys();

            return;
        }

        public async Task GetHeal(float heal, bool show)
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            await Task.Run(() =>
            {
                p_Hp += heal;

                if (p_Hp > p_MaxHP)
                    p_Hp = p_MaxHP;

            });

            if(show)
                await Loging.Log(name + " получил " + heal + " здоровья!", chanel);
        }

        public async Task GetMana(float mana, bool show)
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            await Task.Run(() =>
            {
                p_Mana += mana;

                if (p_Mana > p_MaxMana)
                    p_Mana = p_MaxMana;
            });

            if (show)
                await Loging.Log(name + " получил " + mana + " маны!", chanel);
        }

        public async Task Resurrection()
        {
            var chanel = KingdomRun.client.GetChannel(ulong.Parse(lastRoom)) as IMessageChannel;

            await Task.Run(() =>
            {
                p_Hp = p_MaxHP;
                p_Mana = p_MaxMana;
                ghost = false;
            });

            await Loging.Log(name + " снова в теле!", chanel);
        }

        public async Task ApplyStat(int Defence, int Damage = 0, int Agility = 0, int Strenght = 0, int Intelegence = 0)
        {
            await Task.Run(() =>
            {
                p_Agility += Agility;
                p_Intelegence += Intelegence;
                p_Strength += Strenght;
                p_Attack += Damage;
                p_Defence += Defence;

                RecalkStat();
            });
        }

        private void RecalkStat()
        {
            m_Agility = p_Agility + s_Agility;
            m_Strength = p_Strength + s_Strength;
            m_Intelegence = p_Intelegence + s_Intelegence;

            s_Defence = (int)(m_Agility * 1.1f);

            switch (p_Class)
            {
                case classType.Ассасин:
                    s_Attack = (int)(m_Agility * 1.4f);
                    break;
                case classType.Воин:
                    s_Attack = (int)(m_Strength * 1.3f);
                    break;
                case classType.Лучник:
                    s_Attack = (int)(m_Agility * 0.9f);
                    break;
                case classType.Маг:
                    s_Attack = (int)(m_Intelegence * 0.8f);
                    break;
            }

            p_MaxHP = 100 + ( 10 * m_Strength);

            p_MaxMana = 100 + (10 * m_Intelegence);

            m_Attack = p_Attack + s_Attack;

            m_Defence = p_Defence + s_Defence;
        }

        public async Task GetDefence(int Defence)
        {
            await Task.Run(() =>
            {
                p_Defence += Defence;

                m_Defence = p_Defence + s_Defence;
            });
        }

        public async Task UpdateTime()
        {

            await GetHeal(m_Strength * 0.7f, false);

            await GetMana(m_Intelegence * 0.7f, false);

            for(int i=0;i<buffs.Count;i++)
            {
                buffs[i].time -= 1;

                if (buffs[i].time <= 0)
                {
                    switch (buffs[i].type)
                    {
                        case Buff.buffType.PowerUp:
                            await ApplyStat(0, 0, (int)-buffs[i].value, (int)-buffs[i].value, (int)-buffs[i].value);
                            break;
                        case Buff.buffType.Damage:
                            await ApplyStat(0, (int)-buffs[i].value);
                            break;
                        case Buff.buffType.Defence:
                            await ApplyStat((int)-buffs[i].value);
                            break;
                    }
                    buffs.Remove(buffs[i]);
                    i--;
                }
            }

        }
    }
}

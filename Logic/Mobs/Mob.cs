using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Generators;
using Zamargrad.Logic.Skills.Magic;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Mobs
{
    public class Mob
    {
        public string name;
        public int damage, hp, level, exp, defence;
        public ulong room;

        public Dictionary<string, Person> heroes = new Dictionary<string, Person>();

        public Mob(string name,int level)
        {
            this.name = name;
            this.level = level;

            damage = 5 + (15 * level);
            hp = 60 + (40 * level);
            defence = 5 +(5 * level);

            Random rand = new Random();

            exp = rand.Next(2 * level, 10 * level);
        }
        //Получаем урон
        public async Task GetDamage(Person hero, Magic magic=null)
        {
            var chanel = KingdomRun.client.GetChannel(room) as IMessageChannel;

            if (heroes.TryAdd(hero.name, hero)) heroes.Add(hero.name, hero);

            //Если это не магия
            if (magic == null)
            {
                Random rand = new Random();

                float damag = rand.Next(hero.m_Attack - 10, hero.m_Attack + 10);

                damag = damage - (defence * 0.75f);

                hp = (int)(hp - damag);

                await Loging.Log(name + " получил " + hero.m_Attack + " урона. Осталось " + hp + " здоровья!", chanel);
            }
            else//если все таки магия
            {
                hp = (int)(hp - magic.value);

                await Loging.Log(name + " получил " + magic.value + " урона. Осталось " + hp + " здоровья!", chanel);

                await Loging.Log(name + " теперь " + magic.effect, chanel);
            }

            //Если враг побежден
            if(hp <= 0)
            {
                await Loging.Log(name + " побежден!", chanel);

                float splitExp = exp / heroes.Count;

                //распределяем опыт между игроками
                foreach(KeyValuePair<string,Person> p in heroes)
                {
                    p.Value.fight = false;

                    await p.Value.GetExp((int)splitExp);
                }

                await TravelService.CheckEnemys();

                return;

            }

        }

    }
}

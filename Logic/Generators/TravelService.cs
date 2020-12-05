using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Mobs;
using Zamargrad.Logic.Skills.Magic;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Generators
{
    public static class TravelService
    {

        public static Mob[] enemys = new Mob[4];
        public static bool[] lockWay = new bool[4];


        //Начинаем путешествие
        public static async Task Travel(SocketCommandContext Context)
        {
            //получаем игрока
            Person player = Exeptioner.ValidMe(Context) ? PersonService.persons[Context.User.Username] : null;
            //если игрок не найден
            if (player == null) return;
            //если это не канал "путешествие"
            if (!Context.Channel.Name.Contains("путешествие"))
            {
                await Loging.Log("Путешествовать можно только за стенами города", Context.Channel);
                return;
            }

            string[] travelName = Context.Channel.Name.ToString().Split("путешествие");


            //получаем индекс канала
            int indx = int.Parse(travelName[1]) - 1;


            Random rand = new Random();

            int choose = rand.Next(0, 3);
            //Проверяем есть ли кто на канале
            if (lockWay[indx] == false || indx == player.way)
            {
                //если мы деремся
                if(player.fight == true)
                {
                    await Loging.Log("Ты в бою!", Context.Channel);
                    return;
                }
                //лочим канал
                if(!lockWay[indx])
                    lockWay[indx] = true;

                player.way = indx;
                //Смотрим что нам повстречалось
                switch (choose)
                {
                    case 0:
                        await SetEnemy(Context, indx);
                        break;
                    case 1:
                        await FindLoot(player, Context);
                        break;
                    case 2:
                        await Nothing(player, Context);
                        break;
                }
            }
            else//Если кто-тоесть на канале
                await Loging.Log("Кто-то уже пошел по этой дороге. Если не собираешься помогать, то иди другой дорогой!", Context.Channel);

        }
        //Странные странности
        private static async Task Nothing(Person player, SocketCommandContext Context)
        {
            Random rand = new Random();

            string[] somthing = {
"Птицы пролетели мимо тебя.\n Что бы это могло значить?",
"Солнце светит ярче чем обычно.",
"Запах в воздухе как после дождя.",
"Чувствуется опасность.",
"Деревья шумят под действием ветра.",
"Облака сгущяются",
"В дали что бежит.\n Слишком далеко.\n Не разглядеть."
            };

            await Loging.Log(somthing[rand.Next(0,somthing.Length)], Context.Channel);
        }
        //Нашел лут!
        private static async Task FindLoot(Person player, SocketCommandContext Context)
        {

            Random rand = new Random();

            int exp = rand.Next(20,50);

            await player.GetExp(exp);

            await Loging.Log("Ты нашел мешочек с " + exp + " опыта!", Context.Channel);

        }

        //Атата игроку
        private static async Task SetEnemy(SocketCommandContext Context, int indxChanel)
        {
            Person player = Exeptioner.ValidMe(Context) ? PersonService.persons[Context.User.Username] : null;

            if (player == null) return;

            if (player.fight != false) return;

            Random rand = new Random();

            Mob mob = new Mob("Враг", rand.Next(player.p_Level - 2  < 1 ?1: player.p_Level - 2, player.p_Level + 3));

            mob.room = ulong.Parse(player.lastRoom);

            await Loging.Log("Тебе повстречался : \n"+
                "Имя : "+ mob.name + "\n" +
                "Уровень : " + mob.level + "\n" +
                "Здоровье : " + mob.hp + "\n" +
                "Атака : " + mob.damage, Context.Channel);

            enemys[indxChanel] = mob;

            player.fight = true;

            await AttackEnemy(Context);

        }
        //Стартуем удар
        public static async Task AttackEnemy(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            var player = PersonService.persons[Context.User.Username];

            if (player.fight == false)
            {
                await Loging.Log("Врагов нет по близости!", Context.Channel);
                return;
            }

            await SetAttack(player);
                        
        }
        //Атата мобу
        public static async Task SetAttack(Person player, Magic magic = null)
        {
            var enemy = enemys[player.way];

            await enemy.GetDamage(player, magic);

            await player.GetDamage(enemy.damage);
        }
        //Убираем после себя мобов
        public static async Task CheckEnemys()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < enemys.Length; i++)
                {
                    if (enemys[i].hp <= 0 || enemys[i].heroes.Count == 0)
                        enemys[i] = null;
                }
            });
        }
    }
}

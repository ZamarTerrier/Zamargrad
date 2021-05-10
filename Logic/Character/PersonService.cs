using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Buffs;
using Zamargrad.Logic.Generators;
using Zamargrad.Logic.Items;
using Zamargrad.Logic.Items.Flasks;
using Zamargrad.Logic.Items.Decor;
using Zamargrad.Logic.Items.Use;
using Zamargrad.Logic.Mobs;
using Zamargrad.Logic.Skills;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Character
{
    public class PersonService
    {

        public static Dictionary<string, Person> persons = new Dictionary<string, Person>();

        public static async Task ShowMyStats( SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string text = string.Empty;

            try
            {
                var pi = persons[Context.User.Username];

                text = "Имя : " + pi.name + "\n" +
                    "Титул : " + pi.p_Titul + "\n" +
                    "Класс : " + pi.p_Class + "\n" +
                    "Здоровье : " + pi.p_Hp + "/" + pi.p_MaxHP + "\n" +
                    "Мана : " + pi.p_Mana + "/" + pi.p_MaxMana + "\n" +
                    "Уровень : " + pi.p_Level + "\n" +
                    "Опыт : " + pi.p_Exp + "/" + pi.p_Exp_Need + "\n" +
                    "Урон : " + pi.m_Attack + "\n" +
                    "Защита : " + pi.m_Defence + "\n" +
                    "Ловкость : " + pi.m_Agility + "\n" +
                    "Сила : " + pi.m_Strength + "\n" +
                    "Интелект : " + pi.m_Intelegence + "\n" +
                    "Золото : " + pi.money + "\n";

                await Loging.Log(text, Context.Channel);
            }
            catch
            {
                await Loging.Log("Вы не являетесь членом города Замарград", Context.Channel);
            }
        }

        public static async Task ShowMyItems(SocketCommandContext Context)
        {

            if (!Exeptioner.ValidMe(Context)) return;

            string inv = string.Empty;

            var invent = persons[Context.User.Username].items;

            foreach (KeyValuePair<string, Item> kp in invent)
            {
                inv += kp.Key + " : " + kp.Value.count + " шт.\n";
            }

            if (inv == string.Empty)
                await Loging.Log("В вашем инвентаре ничего нет", Context.Channel);
            else
                await Loging.Log("Ваш инвентарь :\n" + inv, Context.Channel);
        }

        public static async Task ShowMySkills(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string inv = string.Empty;

            var invent = persons[Context.User.Username].skills;

            foreach (KeyValuePair<string, Skill> kp in invent)
            {
                inv += "Название : " + kp.Value.name + "\n" +
                    "Стоимость маны : " + kp.Value.cost + "\n";
            }

            if (inv == string.Empty)
                await Loging.Log("Вы ничего не знаете! Иди учись!", Context.Channel);
            else
                await Loging.Log("Ваши способности :\n" + inv, Context.Channel);
        }

        public static async Task ShowMyBaffs(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string inv = string.Empty;

            var invent = persons[Context.User.Username].buffs;

            foreach (Buff kp in invent)
            {
                inv += "Тип : " + kp.type + "\n" +
                    "Значение : " + kp.value + "\n" +
                    "Осталось времени сек : " + kp.time + "\n";
            }

            if (inv == string.Empty)
                await Loging.Log("На вас нет бафов!", Context.Channel);
            else
                await Loging.Log("Бафы на вас :\n" + inv, Context.Channel);
        }

        internal static async Task Resurrection(SocketCommandContext context, string text)
        {
            if (!Exeptioner.ValidMe(context)) return;

            try
            {
                var player = persons[text];

                if (!player.ghost) return;

                await player.Resurrection();

            }
            catch
            {
                await Loging.Log("Не правильно", context.Channel);
            }

        }
        
        public static async Task Registration(SocketCommandContext Context)
        {

            if (Context.Channel.Name != "регистрационная-палата")
            {
                await Loging.Log("Регистрация проходит исключительно в регистрационной палате!", Context.Channel);
                return;
            }

            if(Exeptioner.ValidMe(Context, false))
            {
                await Loging.Log("Вы уже являетесть членом этого города!", Context.Channel);
                return;
            }

            Person pi = new Person(Context.User.Id.ToString(), Context.User.Username.ToString());
            pi.lastRoom = Context.Channel.Id.ToString();
            persons.Add(pi.name, pi);

            string message = Context.User.Username.ToString() + " теперь полноправный член города!";

            await Loging.Log(message, Context.Channel);
        }

        public static async Task UseItem(string text,SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                var i = persons[Context.User.Username].items[text];

                var ply = persons[Context.User.Username];

                if (i.type == Items.Item.itemType.Use)
                {
                    var useItem = i as UseItem;

                    await Loging.Log(Context.User.Username + " берет в руки " + useItem.name + " и начинает" + useItem.action, Context.Channel);
                }
                else if (i.type == Items.Item.itemType.HealthFlask)
                {
                    var useItem = i as Health;

                    useItem.HealthRegen(ply);


                    await Loging.Log(Context.User.Username + " берет в руки " + useItem.name + " и выпивает.", Context.Channel);

                    if (useItem.count > 1)
                        useItem.count--;
                    else
                        ply.items.Remove(text);

                }
                else if (i.type == Items.Item.itemType.ManaFlask)
                {
                    var useItem = i as Mana;

                    useItem.ManaRegen(ply);

                    await Loging.Log(Context.User.Username + " берет в руки " + useItem.name + " и выпивает.", Context.Channel);

                    if (useItem.count > 1)
                        useItem.count--;
                    else
                        ply.items.Remove(text);

                }
                else
                {
                    var decorItem = i as DecorItem;

                    await Loging.Log(Context.User.Username + " держит в руках " + decorItem.name + " и он чувствует " + decorItem.feel, Context.Channel);
                }
            }
            catch
            {
                await Loging.Log("У тебя нет этого предмета!", Context.Channel);
            }
        }

        public static async Task LookItem(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                var i = persons[Context.User.Username].items[text];

                await Loging.Log("Название : " + i.name + "\n" + i.description, Context.Channel);
            }
            catch
            {
                await Loging.Log("У тебя нет этого предмета!", Context.Channel);
            }
        }

        public static async Task GetClass(string text, SocketCommandContext Context)
        {

            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "регистрационная-палата")
            {
                await Loging.Log("Регистрация проходит исключительно в регистрационной палате!", Context.Channel);
                return;
            }

            try
            {
                var person = persons[Context.User.Username];

                if (person.p_Class != Person.classType.Нету)
                {
                    await Loging.Log("У вас уже есть класс", Context.Channel);
                    return;
                }
                switch (text)
                {
                    case "воин":                        
                        person.p_Class = Person.classType.Воин;
                        await person.SetClass(5,15,3);
                        await Loging.Log(Context.User.Username + " теперь " + person.p_Class, Context.Channel);
                        break;
                    case "маг":
                        person.p_Class = Person.classType.Маг;
                        await person.SetClass(4,3,15);
                        await Loging.Log(Context.User.Username + " теперь " + person.p_Class, Context.Channel);
                        break;
                    case "ассасин":
                        person.p_Class = Person.classType.Ассасин;
                        await person.SetClass(12,8,5);
                        await Loging.Log(Context.User.Username + " теперь " + person.p_Class, Context.Channel);
                        break;
                    case "лучник":
                        person.p_Class = Person.classType.Лучник;
                        await person.SetClass(15,4,5);
                        await Loging.Log(Context.User.Username + " теперь " + person.p_Class, Context.Channel);
                        break;
                    default:
                        await Loging.Log("Не то ты говоришь", Context.Channel);
                        break;
                }
            }
            catch
            {
                await Loging.Log("Ты еще не зарегестрировался в городе!", Context.Channel);
            }
        }

        public static async Task SetRang(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "замок")
            {
                await Loging.Log("Назначение титула проводится исключительно в замке!", Context.Channel);
                return;
            }

            string[] rang = text.Split(",");

            try
            {
                var person = persons[rang[1]];
                person.p_Titul = rang[0];

                await Loging.Log(person.name + " теперь имеет титул " + rang[0], Context.Channel);
            }
            catch
            {
                await Loging.Log("Нет такого челика!", Context.Channel);
            }
               
        }

        public static async Task SellItem(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                if (Context.Channel.Name != "рынок")
                {
                    await Loging.Log("Продавать можно только на рынке!", Context.Channel);
                    return;
                }

                var player = Exeptioner.ValidMe(Context)? persons[Context.User.Username]:null;

                if (player == null) return;

                var item = player.items[text];

                int price = item.description.Length;

                player.money += price;

                player.items.Remove(text);

                await Loging.Log("Вы продали " + text + " за " + price + " золотых.", Context.Channel);

            }catch
            {
                await Loging.Log("У вас нет такого предмета", Context.Channel);
            }
        }

        public static async Task Support(string text, SocketCommandContext Context)
        {
            try
            {
                if (!Exeptioner.ValidMe(Context)) return;

                if (!Exeptioner.ValidPlayer(text, Context)) return;                

                var player = persons[Context.User.Username];

                var comrad = persons[text];

                if (player.lastRoom != comrad.lastRoom)
                {
                    await Loging.Log("Игрок не рядом", Context.Channel);
                    return;
                }

                string log = string.Empty;

                Mob mob = null;

                //Бъет Игрок
                mob = TravelService.enemys[comrad.way];
                await mob.GetDamage(player);

                //Бьет Моб
                int mobDamage = mob.damage;
                await player.GetDamage(mobDamage);

            }
            catch
            {
                await Loging.Log("Так ничего не получится", Context.Channel);
            }
        }

        public static async Task GiveMoney(string text, SocketCommandContext Context)
        {
            try
            {
                string[] moneyText = text.Split(",");

                if (!Exeptioner.ValidMe(Context)) return;

                if (!Exeptioner.ValidPlayer(moneyText[1], Context)) return;

                var player = persons[Context.User.Username];

                var comrad = persons[moneyText[1]];

                int result = 1;

                if(!int.TryParse(moneyText[0], out result)){
                    await Loging.Log("Так ничего не получится", Context.Channel);
                    return;
                }

                if (player.money - int.Parse(moneyText[0]) < 0)
                {
                    await Loging.Log("У тебя нет столько денег", Context.Channel);
                    return;
                }
                else
                {
                    player.money -= int.Parse(moneyText[0]);
                    comrad.money += int.Parse(moneyText[0]);
                }


            }
            catch
            {
                await Loging.Log("Так ничего не получится", Context.Channel);
            }
        }

        public static async Task BackToHome(SocketCommandContext Context)
        {

            if (!Exeptioner.ValidMe(Context)) return;

            var player = persons[Context.User.Username];

            if(player.way == -1)
            {
                await Loging.Log("Ты уже дома!", Context.Channel);

                return;
            }

            if (player.fight)
            {

                await Loging.Log("Ты в бою!", Context.Channel);

                return;

            }

            TravelService.lockWay[player.way] = false;

            player.way = -1;

            await Loging.Log("Возвращаемся домой!", Context.Channel);

        }

    }
}

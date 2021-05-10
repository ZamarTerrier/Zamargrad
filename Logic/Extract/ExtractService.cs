using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Items.Forest;
using Zamargrad.Logic.Items.Ore;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Extract
{
    public class ExtractService
    {

        public static async Task Harvest(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "шахты")
            {
                await Loging.Log("Добывать руду можно только в шахтах!", Context.Channel);
                return;
            }

            var ply = PersonService.persons[Context.User.Username];

            Random rand = new Random();

            int num = rand.Next(0, 100);

            if(num < 35)
            {
                Ore ore = new Ore(Ore.Type.Меди, 1, 30);

                bool added = ply.items.TryAdd(ore.name, ore);

                if (!added)
                    ply.items[ore.name].count++;
                else
                    ply.items[ore.name].count = 1;

                await Loging.Log(ply.name + " выкопал " + ore.name, Context.Channel);
            }
            else if(num < 60)
            {
                Ore ore = new Ore(Ore.Type.Железа, 1, 30);

                bool added = ply.items.TryAdd(ore.name, ore);

                if (!added)
                    ply.items[ore.name].count++;
                else
                    ply.items[ore.name].count = 1;

                await Loging.Log(ply.name + " выкопал " + ore.name, Context.Channel);

            }
            else if(num < 85)
            {
                Ore ore = new Ore(Ore.Type.Золота, 1, 30);

                bool added = ply.items.TryAdd(ore.name, ore);

                if (!added)
                    ply.items[ore.name].count++;
                else
                    ply.items[ore.name].count = 1;

                await Loging.Log(ply.name + " выкопал " + ore.name, Context.Channel);

            }
            else if(num < 100)
            {
                Ore ore = new Ore(Ore.Type.Платины, 1, 30);

                bool added = ply.items.TryAdd(ore.name, ore);

                if (!added)
                    ply.items[ore.name].count++;
                else
                    ply.items[ore.name].count = 1;

                await Loging.Log(ply.name + " выкопал " + ore.name, Context.Channel);

            }

        }

        public static async Task Timber(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "лес")
            {
                await Loging.Log("Добывать дерево можно только в лесу!", Context.Channel);
                return;
            }

            var ply = PersonService.persons[Context.User.Username];

            Random rand = new Random();

            int num = rand.Next(1, 10);

            Timber timber = new Timber("Дерево", 1, 30);


            bool added = ply.items.TryAdd(timber.name, timber);

            if (!added)
                ply.items[timber.name].count += num;
            else
                ply.items[timber.name].count = num;

            await Loging.Log(ply.name + " срубил " + timber.name, Context.Channel);
        }

        public static async Task Hunt(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "лес")
            {
                await Loging.Log("Охотиться можно только в лесу!", Context.Channel);
                return;
            }

            var ply = PersonService.persons[Context.User.Username];

            Random rand = new Random();

            int numMeat = rand.Next(0, 6);
            int numSkin= rand.Next(0, 4);

            if(numMeat == 0 && numSkin == 0)
            {
                await Loging.Log("У " + ply.name + " не вышло поохотиться.", Context.Channel);
                return;
            }else
            {
                bool added = false;

                await Loging.Log(ply.name + " поймал дичь!", Context.Channel);

                if (numMeat > 0)
                {
                    Meat meat = new Meat("Мясо", 1, 30);

                    added = ply.items.TryAdd(meat.name, meat);

                    if (!added)
                        ply.items[meat.name].count += numMeat;
                    else
                        ply.items[meat.name].count = numMeat;

                    await Loging.Log(ply.name + " получил " + meat.name, Context.Channel);
                }

                if (numSkin > 0)
                {
                    Skin skin = new Skin("Кожа", 1, 30);

                    added = ply.items.TryAdd(skin.name, skin);

                    if (!added)
                        ply.items[skin.name].count += numSkin;
                    else
                        ply.items[skin.name].count = numSkin;

                    await Loging.Log(ply.name + " получил " + skin.name, Context.Channel);
                }
            }

        }
    }
}

using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Items;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Shop
{
    public class ShopService : ModuleBase<SocketCommandContext>
    {

        public static List<Item> items = new List<Item>();

        public static async Task MakeShop(SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "рынок")
            {
                await Loging.Log("Товар можно купить только на рынке!", Context.Channel);
                return;
            }
            string all = string.Empty;

            for(int i=0;i < items.Count; i++)
            {
                all += "[" + i + "] | " + "Название : " + items[i].name + " | Цена : " + items[i].cost + "\n";
            }

            if(all == string.Empty)
            {
                all = "На рынке нет товаров!";
            }

            await Loging.Log(all, Context.Channel);

        }

        public static async Task Buyshop(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            int i = 0;

            if(int.TryParse(text, out i))
            {

                var ply = PersonService.persons[Context.User.Username];

                if(ply.money >= items[i].cost)
                {
                    ply.money -= items[i].cost;
                    bool added = ply.items.TryAdd(items[i].name, items[i]);

                    if (!added)
                        ply.items[items[i].name].count++;
                    else
                        ply.items[items[i].name].count = 1;



                    await Loging.Log(ply.name + " приобретает " + items[i].name, Context.Channel);
                }
                else
                {
                    await Loging.Log("Не достаточно денег!", Context.Channel);
                }

            }
            else
            {

                await Loging.Log("Такой позиции нет!", Context.Channel);

            }


        }

    }
}

using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Items.Decor;
using Zamargrad.Logic.Items.Use;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Items
{
    public static class ItemService
    {
        public static Dictionary<string, Item> items = new Dictionary<string, Item>();

        public static async Task CreateItem(string text, SocketCommandContext Context)
        {

            if (Context.Channel.Name != "мастерская")
            {
                await Loging.Log("Предметы можно создавать только мастерской", Context.Channel);
                return;
            }

            if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                string[] textArray = text.Split(",");

                string type = textArray[0];

                string name = textArray[1];
                if (name[0] == ' ')
                    name = name.Substring(1);

                string action = textArray[2];
                if (action[0] == ' ')
                    action = action.Substring(1);

                string decription = textArray[3];
                if (decription[0] == ' ')
                    decription = decription.Substring(1);

                if(decription.Length > 255)
                {
                    await Loging.Log("Слишком длинное описание", Context.Channel);
                    return;
                }

                switch (type)
                {
                    case "декор":
                        items.Add(name, new DecorItem(Item.itemType.Decor, name, action, decription));
                        break;
                    case "актив":
                        items.Add(name, new UseItem(Item.itemType.Use, name, action, decription));
                        break;
                    default:
                        break;
                }
                await Loging.Log("Предмет " + name + " теперь существует", Context.Channel);
            }
            catch
            {
                await Loging.Log("Так ничего не получится", Context.Channel);
            }
        }

        public static async Task GetItem(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            try
            {              

                var i = items[text];
                var p = PersonService.persons[Context.User.Username].items.TryAdd(text, i);

                await Loging.Log(Context.User.Username + " получил " + i.name, Context.Channel);
            }
            catch
            {
                await Loging.Log("Так ничего не получится", Context.Channel);
            }
        }

        public static async Task GiveItem(string text, SocketCommandContext Context)
        {
            string[] textArray = text.Split(",");

            

            if (!Exeptioner.ValidMe(Context) || !Exeptioner.ValidPlayer(textArray[1],Context)) return;


            try
            {
                if (textArray[1][0] == ' ')
                    textArray[1] = textArray[1].Substring(1);

                var i = PersonService.persons[Context.User.Username].items[textArray[0]];
                var p2 = PersonService.persons[textArray[1]].items.TryAdd(text, i);

                PersonService.persons[Context.User.Username].items.Remove(textArray[0]);

                await Loging.Log(Context.User.Username + " передал " + i.name + " "+ textArray[1], Context.Channel);
            }
            catch
            {
                await Loging.Log("Так ничего не получится", Context.Channel);
            }
        }        

    }
}

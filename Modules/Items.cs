using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Items;

namespace Zamargrad.Modules
{
    [Name("Items")]
    public class Items : ModuleBase<SocketCommandContext>
    {
        [Command("создать предмет")]
        [Summary("Создать предмет")]
        public Task CreateItem([Remainder]string text) => ItemService.CreateItem(text, Context);

        [Command("получить")]
        [Summary("получить предмет")]
        public Task GetItem([Remainder]string text) => ItemService.GetItem(text, Context);

        [Command("передать")]
        [Summary("передать предмет")]
        public Task GiveItem([Remainder]string text) => ItemService.GiveItem(text, Context);
        [Command("использовать")]
        [Summary("передать предмет")]
        public Task UseItem([Remainder]string text) => PersonService.UseItem(text, Context);
        [Command("посмотреть на")]
        [Summary("передать предмет")]
        public Task LookItem([Remainder]string text) => PersonService.LookItem(text, Context);

        [Command("продать")]
        [Summary("продать предмет")]
        public Task sell([Remainder]string text) => PersonService.SellItem(text, Context);

    }
}

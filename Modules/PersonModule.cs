using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Character;

namespace Zamargrad.Modules
{
    [Name("Person")]
    public class PersonModule : ModuleBase<SocketCommandContext>
    {
        [Command("регистрация")]
        [Summary("Register")]
        public Task Login() => PersonService.Registration(Context);

        [Command("зарегать класс")]
        [Summary("Выбираем себе класс")]
        public Task regClass([Remainder] string text) => PersonService.GetClass(text,Context);

        [Command("назначить титул")]
        [Summary("Выбираем себе класс")]
        public Task setTitul([Remainder] string text) => PersonService.SetRang(text, Context);

        [Command("инфо")]
        [Summary("инфо о персонаже")]
        public Task Info() => PersonService.ShowMyStats(Context);

        [Command("инвентарь")]
        [Summary("инвентарь персонажа")]
        public Task Inv() => PersonService.ShowMyItems(Context);

        [Command("скилы")]
        [Summary("Заценить свои скилы")]
        public Task ShowSkills() => PersonService.ShowMySkills(Context);

        [Command("продать")]
        [Summary("продать предмет")]
        public Task Sell([Remainder] string text) => PersonService.SellItem(text, Context);

        [Command("дать золотых")]
        [Summary("дать золотые товарищу!")]
        public Task GiveMoney([Remainder] string text) => PersonService.GiveMoney(text, Context);

        [Command("дать титул")]
        [Summary("Дать игроку титул")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task SetTitul([Remainder]string text) => PersonService.SetRang(text, Context);

        [Command("воскресить")]
        [Summary("Поднять товарища")]
        public Task Recovery([Remainder]string text) => PersonService.Resurrection(Context, text);

    }
}

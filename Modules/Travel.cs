using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Generators;

namespace Zamargrad.Modules
{
    [Name("Travel")]
    public class Travel : ModuleBase<SocketCommandContext>
    {
        [Command("путешествие")]
        [Summary("В путь!")]
        public Task Login() => TravelService.Travel(Context);

        [Command("атаковать")]
        [Summary("В атаку!")]
        public Task Attack() => TravelService.AttackEnemy(Context);

        [Command("помочь")]
        [Summary("Помочь товарищу!")]
        public Task Attack([Remainder] string text) => PersonService.Support(text, Context);

        [Command("домой")]
        [Summary("Вернуться домой")]
        public Task Back() => PersonService.BackToHome(Context);
                     
    }
}

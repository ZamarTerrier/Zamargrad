using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Tools.DevTools;

namespace Zamargrad.Modules
{
    [Name("Admin")]
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        [Command("тест")]
        [Summary("Make the bot say something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder]string text) => SendMessage(text);

        [Command("божественная чистка")]
        [Summary("Почистить вселенную")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task UnlockWays() => AdminService.ClearAll(Context);

        private async Task SendMessage(string text)
        {
            await Context.Channel.SendMessageAsync("Something");
        }
    }
}

using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Help;

namespace Zamargrad.Modules
{
    [Name("Help")]
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("помощь")]
        [Summary("Make the bot say something")]
        public Task CreateHelp([Remainder] string text) => Helper.Help(text, Context);
        [Command("помощь")]
        [Summary("Make the bot say something")]
        public Task CreateHelp() => Helper.Help( Context);

    }
}

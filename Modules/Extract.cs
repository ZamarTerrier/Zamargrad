using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Extract;

namespace Zamargrad.Modules
{
    [Name("Extract")]
    public class Extract : ModuleBase<SocketCommandContext>
    {
        [Command("добывать")]
        [Summary("Добыть руду")]
        public Task Harvest() => ExtractService.Harvest(Context);

        [Command("рубить")]
        [Summary("Рубить дерево")]
        public Task Timber() => ExtractService.Timber(Context);

        [Command("охота")]
        [Summary("Пойти на охоту")]
        public Task Hunt() => ExtractService.Hunt(Context);

    }
}

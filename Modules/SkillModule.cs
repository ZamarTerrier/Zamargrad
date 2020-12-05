using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Skills.Magic;
using Zamargrad.Logic.Skills.Power;
using Zamargrad.Logic.Skills.Actions;
using Zamargrad.Logic.Skills;

namespace Zamargrad.Modules
{
    [Name("Skills")]
    public class SkillModule : ModuleBase<SocketCommandContext>
    {
        [Command("создать магию"), Alias("s")]
        [Summary("Создать магию")]
        public Task CreateMagic([Remainder]string text) => MagicService.CreateMagic(text, Context);

        [Command("изучить магию"), Alias("s")]
        [Summary("Изучить магию")]
        public Task LearnMagic([Remainder]string text) => SkillService.GetMagic(text, Context);

        [Command("создать усиление"), Alias("s")]
        [Summary("Создать усиление")]
        public Task CreatePower([Remainder]string text) => PowerService.CreatePower(text, Context);

        [Command("изучить усиление"), Alias("s")]
        [Summary("Изучить усиление")]
        public Task LearnPower([Remainder]string text) => SkillService.GetPower(text, Context);

        [Command("создать действие"), Alias("s")]
        [Summary("Создать действие")]
        public Task CreateAction([Remainder]string text) => ActionService.CreateAction(text, Context);

        [Command("кастануть"), Alias("s")]
        [Summary("Использовать магию")]
        public Task Cast([Remainder]string text) => MagicService.UseMagic(Context, text);

        [Command("наложить"), Alias("s")]
        [Summary("Наложить скилл")]
        public Task Concentrate([Remainder]string text) => PowerService.UsePower(Context, text);        

        [Command("решил"), Alias("s")]
        [Summary("Что-то сделать")]
        public Task UseAction([Remainder]string text) => ActionService.UseAction(Context, text);      

    }
}

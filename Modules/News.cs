using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zamargrad.Modules
{
    [Name("News")]
    public class News: ModuleBase<SocketCommandContext>
    {
        [Command("тест")]
        [Summary("Make the bot say something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder]string text) => SendMessage(text);


        private async Task SendMessage(string text)
        {
            await Task.Run(() =>
            {
                foreach(SocketGuild g in KingdomRun.client.Guilds)
                {
                    foreach(SocketGuildChannel gc in g.Channels)
                    {
                        Console.WriteLine(gc.Name);
                        Console.WriteLine(gc.Id);
                    }
                }

                var chanel = KingdomRun.client.GetChannel(451819221505277955) as IMessageChannel;

                chanel.SendMessageAsync(text);

            });
        }
    }
}

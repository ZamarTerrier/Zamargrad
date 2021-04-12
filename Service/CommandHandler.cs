using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Service
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;

        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;     // Ensure the message is from a user/bot
            if (msg == null) return;
            if (msg.Author.Id == _discord.CurrentUser.Id) return;     // Ignore self when checking commands

            var context = new SocketCommandContext(_discord, msg);     // Create the command context

            try
            {
                var ply = PersonService.persons[context.User.Username.ToString()];

                if (context.Channel.Id.ToString() != ply.lastRoom)
                {

                    if (ply.fight)
                    {
                        await ply.SmashPly(ply.name + " попытался сбежать, но был убит!");
                        
                        return;
                    }

                    var chanel = KingdomRun.client.GetChannel(ulong.Parse(ply.lastRoom)) as IMessageChannel;

                    await chanel.SendMessageAsync(context.User.Username.ToString() + " покинул " + chanel.Name);

                    ply.lastRoom = context.Channel.Id.ToString();

                    Console.WriteLine(context.User.Username.ToString() + " теперь в " + context.Channel.Name);
                }
            }
            catch
            {
                Console.WriteLine("Не зарегестрированный человек");
            }

            int argPos = 0;     // Check if the message has a valid command prefix
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);     // Execute the command

                if (!result.IsSuccess)     // If not successful, reply with the error.
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}

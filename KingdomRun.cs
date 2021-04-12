using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Threading.Tasks;

using Zamargrad.Service;
using Zamargrad.Logic.Shop;
using Zamargrad.Logic.Items.Flasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Zamargrad
{
    public class KingdomRun
    {
        private bool loggined;
        private string[] mesgs =
        {
            "Всем приятной игры!",
            "Не забывайте мыть руки!",
            "Будьте вежливы с жителями города!",
            "Не забудь рассказать друзьям!",
            "Бойтесь ящериц, они использут канализацию\n, для перемещению по городу!",
            "Старайтесь закупать как можно больше эллексиров\n они вам пригодятся!"
        };
        private Random rand;

        public static DiscordSocketClient client;
        public IConfigurationRoot Configuration { get; }

        public KingdomRun()
        {
            rand = new Random();

            var builder = new ConfigurationBuilder()        // Create a new instance of the config builder
                .SetBasePath(AppContext.BaseDirectory)      // Specify the default location for the config file
                .AddJsonFile("_config.json");              // Add this (yaml encoded) file to the configuration

            Configuration = builder.Build();

            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                MessageCacheSize = 1000
            });

            client.Ready += StartMessage;
        }

        private async Task StartMessage()
        {
            ulong key = 0;
            
            foreach(SocketGuild g in client.Guilds)
            {
                foreach (SocketGuildChannel gc in g.Channels)
                {
                    if(gc.Name == "доска-объявлений")
                    {
                        key = gc.Id;
                        break;
                    }
                }
            }

                var chanel = client.GetChannel(key) as IMessageChannel;

            if (!loggined)
            {

                ShopService.items.Add(new Mana("маленький флакон маны", 50, 100));
                ShopService.items.Add(new Mana("средний флакон маны", 100, 200));
                ShopService.items.Add(new Mana("большой флакон маны", 150, 300));

                ShopService.items.Add(new Mana("маленький флакон здоровья", 70, 120));
                ShopService.items.Add(new Mana("средний флакон здоровья", 120, 220));
                ShopService.items.Add(new Mana("большой флакон здоровья", 170, 320));

                await chanel.SendMessageAsync("Имерия снова в деле!");
                loggined = true;
            }
            else
            {
                try
                {
                    int m = rand.Next(0, mesgs.Length);

                    await chanel.SendMessageAsync(mesgs[m]);

                }
                catch
                {

                }
            }
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();             // Create a new instance of a service collection
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();     // Build the service provider
            provider.GetRequiredService<LoggingService>();      // Start the logging service
            provider.GetRequiredService<CommandHandler>(); 		// Start the command handler service

            await provider.GetRequiredService<StartupService>().StartAsync();       // Start the startup service
            await Task.Delay(-1);                               // Keep the program alive
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(client).AddSingleton(new CommandService(new CommandServiceConfig
            {                                       // Add the command service to the collection
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
            }))
            .AddSingleton<CommandHandler>()         // Add the command handler to the collection
            .AddSingleton<StartupService>()         // Add startupservice to the collection
            .AddSingleton<LoggingService>()         // Add loggingservice to the collection
            .AddSingleton<Random>()
            .AddSingleton(Configuration);           // Add the configuration to the collection
        }
    }
}

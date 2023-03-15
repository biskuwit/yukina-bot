using Discord.Commands;
using Discord.WebSocket;
using Discord;
using YukinaBot.Helpers;

namespace YukinaBot
{
    public class Client
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task MainAsync()
        {
            /* Load up environment variables from file */
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);
            _client.Log += Log;

            _handler = new CommandHandler(_client, new CommandService());
            await _handler.InstallCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, ConfigurationHelper.GetToken());
            await _client.StartAsync();
            await _client.SetGameAsync($"{ConfigurationHelper.GetBotPrefix()}help", null, ActivityType.Listening);

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}

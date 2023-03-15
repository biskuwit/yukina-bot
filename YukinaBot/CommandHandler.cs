using Discord;
using Discord.Commands;
using Discord.WebSocket;
using YukinaBot.Helpers;

namespace YukinaBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            if (messageParam is not SocketUserMessage message) return;

            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            var prefix = ConfigurationHelper.GetBotPrefix();
            if (!(message.HasCharPrefix(Convert.ToChar(ConfigurationHelper.GetBotPrefix() ?? throw new InvalidOperationException("Missing bot prefix setting.")), ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
            {
                return;
            }

            Console.WriteLine($"\n{message.Author.Username} sent: {message.Content}\n");

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            var result = await _commands.ExecuteAsync(context, argPos, null);
            if (!result.IsSuccess)
            {
                Console.WriteLine($"{result.ErrorReason}");

                if (result.Error.Equals(CommandError.UnknownCommand))
                {
                    await context.Message.AddReactionAsync(new Emoji("\u2753"));
                    return;
                }

                if (result.Error.Equals(CommandError.BadArgCount))
                {
                    await context.Channel.SendMessageAsync($"`{context.Message.Content}` requires more parameters.");
                    return;
                }

                await context.Message.AddReactionAsync(new Emoji("\uD83D\uDEAB"));
            }
        }
    }
}

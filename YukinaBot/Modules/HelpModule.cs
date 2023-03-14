using Discord.Commands;
using Discord;
using YukinaBot.Helpers;

namespace YukinaBot.Modules
{
    [Group("help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command]
        [Summary("Shows and overview of the bot commands.")]
        public Task HelpAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Commands Overview")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search `CRITERIA`", "Search AniList's database for media based on the given criteria. Returns a list of entries.", false)
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find `CRITERIA`", "Finds one piece of media from AniList's database.", false)
                .WithDescription($"For more descriptive help, type {ConfigurationHelper.GetBotPrefix()}help `COMMAND`")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("search")]
        [Summary("Shows help for the search command.")]
        public Task HelpSearchAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}search")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search anime `CRITERIA`", "Specify the search to anime only.", false)
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search manga `CRITERIA`", "Specify the search to manga only.", false)
                .WithDescription($"Searches through AniList's database to find media based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}search sword art online`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("find")]
        [Summary("Shows help for the find command.")]
        public Task HelpFindAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}find")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find anime `CRITERIA`", "Specify the find to anime only.", false)
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find manga `CRITERIA`", "Specify the find to manga only.", false)
                .WithDescription($"Finds a single piece of media based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}find sword art online`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }
    }
}

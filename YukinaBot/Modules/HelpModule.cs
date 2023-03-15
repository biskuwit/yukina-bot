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
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search `CRITERIA`", "Search AniList's database for media based on the given criteria. Returns a list of entries.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find `CRITERIA`", "Finds one piece of media from AniList's database.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}anime `CRITERIA`", "Finds one piece of anime from AniList's database.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}manga `CRITERIA`", "Finds one piece of manga from AniList's database.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}user `ANILIST_USERNAME`", "Shows a User's Anilist statistics.")
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
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search anime `CRITERIA`", "Specify the search to anime only.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}search manga `CRITERIA`", "Specify the search to manga only.")
                .WithDescription($"Searches through AniList's database to find media based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}search bleach`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("find")]
        [Summary("Shows help for the find command.")]
        public Task HelpFindAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}find")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find anime `CRITERIA`", "Specify the find to anime only.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find manga `CRITERIA`", "Specify the find to manga only.")
                .WithDescription($"Finds a single piece of media based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}find bleach`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("anime")]
        [Summary("Shows help for the anime command.")]
        public Task HelpAnimeAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}anime")
                .WithDescription($"Finds a single piece of anime based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}anime bleach`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("manga")]
        [Summary("Shows help for the manga command.")]
        public Task HelpMangaAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}manga")
                .WithDescription($"Finds a single piece of manga based on the given criteria.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}manga bleach`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("user")]
        [Summary("Shows help for the user command.")]
        public Task HelpUserAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}user")
                .WithDescription($"Finds the user with the given username and displays their anime & manga list statistics.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}user <ANILIST USERNAME>`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }
    }
}

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
                .AddField($"{ConfigurationHelper.GetBotPrefix()}setup anilist `ANILIST_USERNAME`", "Adds a User's Anilist to the database for future usage.")
                .WithDescription($"For more descriptive help, type {ConfigurationHelper.GetBotPrefix()}help `COMMAND`")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("search")]
        [Summary("Shows help for the search command.")]
        [Alias("search anime", "search manga")]
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
        [Alias("find anime", "find manga", "fetch", "fetch anime", "fetch manga", "get", "get anime", "get manga", "media", "media anime", "media manga")]
        public Task HelpFindAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}find")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find `CRITERIA`", "Specify the find to anime only based on search criteria.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find anime `CRITERIA`", "Specify the find to anime only based on search criteria.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find anime `ID`", "Specify the find to anime only based on the Anilist ID.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find manga `CRITERIA`", "Specify the find to manga only based on search criteria.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}find manga `ID`", "Specify the find to manga only based on the Anilist ID.")
                .WithDescription($"Finds a single piece of media based on the given criteria or ID.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}find fullmetal alchemist brotherhood`\n\n_{builder.Fields.Count} overloads exist for this command._\n\nAliases: [get, fetch, media]")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("anime")]
        [Summary("Shows help for the anime command.")]
        public Task HelpAnimeAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}anime")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}anime `CRITERIA`", "Find a single anime based on criteria.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}anime `ID`", "Find a single anime based on ID.")
                .WithDescription($"Finds a single piece of anime based on the given criteria or ID.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}anime kimi no na wa`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("manga")]
        [Summary("Shows help for the manga command.")]
        public Task HelpMangaAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}manga")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}manga `CRITERIA`", "Find a single manga based on criteria.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}manga `ID`", "Find a single manga based on ID.")
                .WithDescription($"Finds a single piece of manga based on the given criteria or ID.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}manga sword art online`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("user")]
        [Summary("Shows help for the user command.")]
        [Alias("list", "userlist")]
        public Task HelpUserAsync()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}user")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}user `USERNAME`", "Specify the username of the user.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}user `ID`", "Specify the id of the user.")
                .WithDescription($"Finds the user with the given username or ID and displays their anime & manga list statistics.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}user SmellyAlex`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }

        [Command("setup")]
        [Summary("Shows help for the setup command.")]
        [Alias("setup anilist", "setup edit", "setup update")]
        public Task HelpSetupAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle($"{ConfigurationHelper.GetBotPrefix()}setup")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}setup anilist `USERNAME`", "Looks for the Anilist user based on username and adds it to the database.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}setup anilist `ID`", "Looks for the Anilist user based on ID and adds it to the database.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}setup update `USERNAME`", "Edits the User's Anilist information in the database based on new Anilist username.")
                .AddField($"{ConfigurationHelper.GetBotPrefix()}setup update `ID`", "Edits the User's Anilist information in the database based on new Anilist ID.")
                .WithDescription($"Adds a User's Anilist to the database for future usage.\n\nExample usage: `{ConfigurationHelper.GetBotPrefix()}setup anilist 273353`\n\n_{builder.Fields.Count} overloads exist for this command._")
                .WithColor(Color.DarkRed);

            return ReplyAsync("", false, builder.Build());
        }
    }
}

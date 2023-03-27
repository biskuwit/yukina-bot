using Discord.Commands;

namespace YukinaBot.Modules
{
    public class ListModule : AbstractModule
    {
        [Command("user")]
        [Summary("Gets statistics about a user using their username.")]
        [Alias("list", "userlist")]
        public async Task GetAnimeListAsync([Remainder] string userName)
        {
            var userResponse = await AniListFetcher.FindUserStatisticsAsync(userName);
            await ReplyAsync("", false, EmbedUtility.BuildUserEmbed(userResponse.User));
        }

        [Command("user")]
        [Summary("Gets statistics about a user using their AniList ID.")]
        [Alias("list", "userlist")]
        public async Task GetAnimeListAsync([Remainder] int userId)
        {
            var userResponse = await AniListFetcher.FindUserStatisticsAsync(userId);
            await ReplyAsync("", false, EmbedUtility.BuildUserEmbed(userResponse.User));
        }
    }
}

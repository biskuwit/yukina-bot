using Discord.Commands;

namespace YukinaBot.Modules
{
    public class ListModule : AbstractModule
    {
        [Command("user")]
        [Summary("Gets statistics about a user.")]
        public async Task GetAnimeListAsync([Remainder] string userName)
        {
            var userResponse = await AniListFetcher.FindUserStatisticsAsync(userName);
            await ReplyAsync("", false, EmbedUtility.BuildUserEmbed(userResponse.User));
        }
    }
}

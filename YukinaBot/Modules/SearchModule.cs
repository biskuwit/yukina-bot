using Discord.Commands;
using System.Text;
using YukinaBot.Enums.AniList;
using YukinaBot.Models.AniList;

namespace YukinaBot.Modules
{
    [Group("search")]
    public class SearchModule : AbstractModule
    {
        [Command]
        [Summary("Search a list of media from AniList GraphQL.")]
        public async Task SearchAsync([Remainder] string searchCriteria)
        {
            string[] arguments = searchCriteria.Split(' ');
            switch (arguments[0])
            {
                case "anime":
                    await SearchAnimeAsync(string.Join(' ', arguments.Skip(1)));
                    break;
                case "manga":
                    await SearchMangaAsync(string.Join(' ', arguments.Skip(1)));
                    break;
                default:
                    var pageResponse = await AniListFetcher.SearchMediaAsync(searchCriteria);
                    await ReplyWithMedia(pageResponse.Page?.Media);
                    break;
            }
        }

        [Command("anime")]
        [Summary("Search a list of anime media from AniList GraphQL.")]
        public async Task SearchAnimeAsync([Remainder] string searchCriteria)
        {
            var pageResponse = await AniListFetcher.SearchMediaTypeAsync(searchCriteria, MediaType.Anime.ToString().ToUpper());
            await ReplyWithMedia(pageResponse.Page?.Media);
        }

        [Command("manga")]
        [Summary("Search a list of manga media from AniList GraphQL.")]
        public async Task SearchMangaAsync([Remainder] string searchCriteria)
        {
            var pageResponse = await AniListFetcher.SearchMediaTypeAsync(searchCriteria, MediaType.Manga.ToString().ToUpper());
            await ReplyWithMedia(pageResponse.Page?.Media);
        }

        private async Task ReplyWithMedia(List<Media>? mediaList)
        {
            // Return out of the method and send a message when there were no results.
            if (mediaList is {Count: 0})
            {
                await ReplyAsync("No media found with this search.");
                return;
            }

            // Sort the media on title.
            if (mediaList != null)
            {
                mediaList = new List<Media>(mediaList.OrderBy(m => m.Title?.English));

                var stringBuilder = new StringBuilder();
                stringBuilder.Append("```\n");

                foreach (var media in mediaList)
                {
                    stringBuilder.Append($"{media.Type} {media.Id}: {media.Title?.English ?? media.Title?.Romaji}\n");
                }

                stringBuilder.Append("```\n");

                await ReplyAsync(stringBuilder.ToString());
            }
        }
    }
}

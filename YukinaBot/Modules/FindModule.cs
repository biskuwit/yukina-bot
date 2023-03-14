using Discord.Commands;
using YukinaBot.Enums.AniList;

namespace YukinaBot.Modules
{
    public class FindModule : AbstractModule
    {
        [Command("find")]
        [Alias("fetch")]
        [Summary("Find media from AniList GraphQL.")]
        public async Task FindAsync([Remainder] string searchCriteria)
        {
            // Redirect to anime or manga find based on if there is a second parameter "anime" or "manga".
            if (searchCriteria.Split(' ')[0] == "anime")
            {
                await FindAnimeAsync(string.Join(' ', searchCriteria.Split(' ').Skip(1)));
                return;
            }

            if (searchCriteria.Split(' ')[0] == "manga")
            {
                await FindMangaAsync(string.Join(' ', searchCriteria.Split(' ').Skip(1)));
                return;
            }

            var mediaResponse = await AniListFetcher.FindMediaAsync(searchCriteria);

            await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media));
        }

        [Command("anime")]
        [Summary("Find anime media from AniList GraphQL.")]
        public async Task FindAnimeAsync([Remainder] string searchCriteria)
        {
            var mediaResponse = await AniListFetcher.FindMediaTypeAsync(searchCriteria, MediaType.Anime.ToString());

            await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media));
        }

        [Command("manga")]
        [Summary("Find manga media from AniList GraphQL.")]
        public async Task FindMangaAsync([Remainder] string searchCriteria)
        {
            var mediaResponse = await AniListFetcher.FindMediaTypeAsync(searchCriteria, MediaType.Manga.ToString());

            await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media));
        }
    }
}

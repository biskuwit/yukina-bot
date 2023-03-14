﻿using Discord.Commands;
using System.Text;
using YukinaBot.Enums;

namespace YukinaBot.Modules
{
    [Group("search")]
    public class SearchModule : AbstractModule
    {
        [Command]
        [Summary("Search a list of media from AniList GraphQL.")]
        public async Task SearchAsync([Remainder] string searchCriteria)
        {
            // Redirect to anime or manga search based on if there is a second parameter "anime" or "manga".
            if (searchCriteria.Split(' ')[0] == "anime")
            {
                await SearchAnimeAsync(string.Join(' ', searchCriteria.Split(' ').Skip(1)));
                return;
            }

            if (searchCriteria.Split(' ')[0] == "manga")
            {
                await SearchMangaAsync(string.Join(' ', searchCriteria.Split(' ').Skip(1)));
                return;
            }

            var pageResponse = await AniListFetcher.SearchMediaAsync(searchCriteria);

            // Return out of the method and send a message when there were no results.
            if (pageResponse.Page?.Media is {Count: 0})
            {
                await ReplyAsync("No media found with this search.");
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("```\n");

            if (pageResponse.Page.Media != null)
                foreach (var media in pageResponse.Page.Media)
                    stringBuilder.Append(
                        $"{media.Type} {media.Id}: {media.Title?.English ?? media.Title?.Romaji}\n");

            stringBuilder.Append("```\n");
            await ReplyAsync(stringBuilder.ToString());
        }

        [Command("anime")]
        [Summary("Search a list of anime media from AniList GraphQL.")]
        public async Task SearchAnimeAsync([Remainder] string searchCriteria)
        {
            var pageResponse = await AniListFetcher.SearchMediaTypeAsync(searchCriteria, MediaType.Anime.ToString());

            // Return out of the method and send a message when there were no results.
            if (pageResponse.Page?.Media is {Count: 0})
            {
                await ReplyAsync("No anime found with this search.");
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("```\n");

            if (pageResponse.Page?.Media != null)
                foreach (var media in pageResponse.Page.Media)
                    stringBuilder.Append(
                        $"{media.Type} {media.Id}: {media.Title?.English ?? media.Title?.Romaji}\n");

            stringBuilder.Append("```\n");
            await ReplyAsync(stringBuilder.ToString());
        }

        [Command("manga")]
        [Summary("Search a list of manga media from AniList GraphQL.")]
        public async Task SearchMangaAsync([Remainder] string searchCriteria)
        {
            var pageResponse = await AniListFetcher.SearchMediaTypeAsync(searchCriteria, MediaType.Manga.ToString());

            // Return out of the method and send a message when there were no results.
            if (pageResponse.Page?.Media is {Count: 0})
            {
                await ReplyAsync("No manga found with this search.");
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("```\n");

            if (pageResponse.Page?.Media != null)
                foreach (var media in pageResponse.Page.Media)
                    stringBuilder.Append(
                        $"{media.Type} {media.Id}: {media.Title?.English ?? media.Title?.Romaji}\n");

            stringBuilder.Append("```\n");

            await ReplyAsync(stringBuilder.ToString());
        }
    }
}
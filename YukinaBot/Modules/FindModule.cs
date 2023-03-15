﻿using Discord.Commands;
using MongoDB.Bson;
using MongoDB.Driver;
using YukinaBot.Enums.AniList;
using YukinaBot.Models.AniList;
using YukinaBot.Models;
using YukinaBot.Enums;
using YukinaBot.ResponseModels;

namespace YukinaBot.Modules
{
    public class FindModule : AbstractModule
    {
        [Command("find")]
        [Alias("fetch", "get", "media")]
        [Summary("Find media from AniList GraphQL.")]
        public async Task FindAsync([Remainder] string searchCriteria)
        {
            var arguments = searchCriteria.Split(' ');
            switch (arguments[0])
            {
                case "anime":
                    await FindAnimeAsync(string.Join(' ', arguments.Skip(1)));
                    break;
                case "manga":
                    await FindMangaAsync(string.Join(' ', arguments.Skip(1)));
                    break;
                default:
                    var mediaResponse = await AniListFetcher.FindMediaAsync(searchCriteria);
                    var embedMediaList = FetchMediaStatsForUser(mediaResponse.Media);
                    await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media, embedMediaList));
                    break;
            }
        }
        [Command("anime")]
        [Summary("Find anime media from AniList GraphQL.")]
        public async Task FindAnimeAsync([Remainder] string searchCriteria)
        {
            var mediaResponse = await AniListFetcher.FindMediaTypeAsync(searchCriteria, MediaType.Anime.ToString().ToUpper());
            var embedMediaList = FetchMediaStatsForUser(mediaResponse.Media);
            await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media, embedMediaList));
        }

        [Command("manga")]
        [Summary("Find manga media from AniList GraphQL.")]
        public async Task FindMangaAsync([Remainder] string searchCriteria)
        {
            var mediaResponse = await AniListFetcher.FindMediaTypeAsync(searchCriteria, MediaType.Manga.ToString().ToUpper());
            var embedMediaList = FetchMediaStatsForUser(mediaResponse.Media);
            await ReplyAsync("", false, EmbedUtility.BuildAnilistMediaEmbed(mediaResponse.Media, embedMediaList));
        }

        private List<EmbedMedia> FetchMediaStatsForUser(Media? media)
        {
            // Fetch users from database
            var db = MongoClient.GetDatabase("YukinaBot");
            var usersCollection = db.GetCollection<DiscordUser>("users");
            var users = usersCollection.Find(new BsonDocument()).ToList();
            var embedMediaList = new List<EmbedMedia>();

            foreach (var user in users)
            {
                var discordUser = MongoDbUtility.FindDiscordUserInChannel(Context.Channel, user.discordId);
                if (discordUser == null)
                    continue;

                var countBefore = embedMediaList.Count;
                var mediaList = AniListFetcher.FindUserListAsync(user.anilistName, media.Type.ToString().ToUpper()).Result.MediaListCollection;

                foreach (var listGroup in mediaList.Lists)
                {
                    foreach (var entry in listGroup.Entries)
                    {
                        // Add entry with fields if found
                        if (entry.MediaId.Equals(media.Id))
                        {
                            var embedMedia = new EmbedMedia
                            {
                                DiscordName = discordUser.Username,
                                Progress = entry.Progress,
                                Score = entry.Score,
                            };

                            Enum.TryParse(entry.Status.ToString(), out EmbedMediaListStatus parsedStatus);
                            embedMedia.Status = parsedStatus;
                            embedMediaList.Add(embedMedia);
                            break;
                        }
                    }
                }

                // Add unwatched if count is still the same
                if (countBefore.Equals(embedMediaList.Count))
                {
                    var embedMedia = new EmbedMedia
                    {
                        DiscordName = discordUser.Username,
                        Progress = 0,
                        Score = 0,
                        Status = EmbedMediaListStatus.NotOnList
                    };
                    embedMediaList.Add(embedMedia);
                }
            }

            return embedMediaList;
        }
    }
}

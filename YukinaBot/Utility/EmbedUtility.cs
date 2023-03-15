using Discord;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using YukinaBot.Enums;
using YukinaBot.Enums.AniList;
using YukinaBot.Models;
using YukinaBot.Models.AniList;

namespace YukinaBot.Utility
{
    public class EmbedUtility
    {
        public Embed BuildAnilistMediaEmbed(Media? media, List<EmbedMedia>? embedMediaList)
        {
            var embedBuilder = new EmbedBuilder();
            var season = media.Season != null ? media.Season.ToString() : "?";
            var seasonYear = media.SeasonYear != null ? media.SeasonYear.ToString() : "?";

            // First row.
            embedBuilder.WithTitle(media.Title.English ?? media.Title.Romaji)
                .AddField("**Type**", media.Type, true)
                .AddField("**Status**", media.Status, true)
                .AddField("**Season**", $"{season} {seasonYear}", true);

            // Second row.
            embedBuilder.AddField("**Anilist Score**", media.MeanScore != null ? $"{media.MeanScore}/100" : "-", true)
                .AddField("**Popularity**", media.Popularity, true)
                .AddField("**Favorited**", $"{media.Favorites} times", true);

            // Third row differs for Anime and Manga.
            if (media.Type == MediaType.Anime)
                embedBuilder.AddField("**Episodes**", media.Episodes != null ? $"{media.Episodes}" : "?", true)
                    .AddField("**Duration**", media.Duration != null ? $"{media.Duration} minutes per episode" : "?", true);
            else
                embedBuilder.AddField("**Volumes**", media.Volumes != null ? $"{media.Volumes}" : "?", true)
                    .AddField("**Chapters**", media.Chapters != null ? $"{media.Chapters}" : "?", true);

            // Fourth row.
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("`");
            stringBuilder.Append(string.Join("` - `", media.Genres));
            stringBuilder.Append("`");
            embedBuilder.AddField("**Genres**", stringBuilder.ToString());

            // Fifth row with user statistics.
            if (embedMediaList != null && embedMediaList.Count != 0)
            {
                stringBuilder.Clear();

                var completedStringBuilder = new StringBuilder();
                var plannedStringBuilder = new StringBuilder();
                var inProgressStringBuilder = new StringBuilder();
                var droppedStringBuilder = new StringBuilder();
                var notOnListStringBuilder = new StringBuilder();

                foreach (var embedMedia in embedMediaList.OrderBy(s => s.Progress).ThenBy(s => s.DiscordName))
                {
                    switch (embedMedia.Status)
                    {
                        case EmbedMediaListStatus.Completed:
                            completedStringBuilder.Append($"{embedMedia.DiscordName} [{embedMedia.Score}/100] ~ ");
                            break;
                        case EmbedMediaListStatus.Current:
                            inProgressStringBuilder.Append($"{embedMedia.DiscordName} [{embedMedia.Progress}] ~ ");
                            break;
                        case EmbedMediaListStatus.Dropped:
                            droppedStringBuilder.Append($"{embedMedia.DiscordName} [{embedMedia.Progress}] ~ ");
                            break;
                        case EmbedMediaListStatus.Paused:
                            inProgressStringBuilder.Append($"{embedMedia.DiscordName} [{embedMedia.Progress}] ~ ");
                            break;
                        case EmbedMediaListStatus.Planning:
                            plannedStringBuilder.Append($"{embedMedia.DiscordName} ~ ");
                            break;
                        default:
                            notOnListStringBuilder.Append($"{embedMedia.DiscordName} ~ ");
                            break;
                    }
                }

                var inProgress = inProgressStringBuilder.ToString().TrimEnd(' ', '~');
                var completed = completedStringBuilder.ToString().TrimEnd(' ', '~');
                var dropped = droppedStringBuilder.ToString().TrimEnd(' ', '~');
                var planned = plannedStringBuilder.ToString().TrimEnd(' ', '~');
                var notOnList = notOnListStringBuilder.ToString().TrimEnd(' ', '~');

                stringBuilder.Append($"**In-Progress**: {inProgress}\n");
                stringBuilder.Append($"**Completed**: {completed}\n");
                stringBuilder.Append($"**Dropped**: {dropped}\n");
                stringBuilder.Append($"**Planning**: {planned}\n");
                stringBuilder.Append($"**Not on List**: {notOnList}");

                embedBuilder.AddField("**User Scores**", stringBuilder.ToString());
            }

            if (media.Description != null)
            {
                // Remove all the HTML elements from the description.
                embedBuilder.WithDescription($"[MyAnimeList Alternative](https://myanimelist.net/anime/{media.IdMal})\n\n_{Regex.Replace(media.Description, "(<\\/?\\w+>)", " ")}_");
            }

            // Add all extra properties.
            embedBuilder.WithColor(Color.Green)
                // .WithCurrentTimestamp()
                .WithThumbnailUrl(media.CoverImage.ExtraLarge)
                .WithUrl(media.SiteUrl);

            return embedBuilder.Build();
        }

        public Embed BuildUserEmbed(User? user, bool withAnime = true, bool withManga = true)
        {
            var embedBuilder = new EmbedBuilder();
            var stringBuilder = new StringBuilder();

            // Build custom description for displaying Anime
            if (withAnime)
            {
                stringBuilder.Append($"\n[**Anime List**]({user.SiteUrl}/animelist)\n");
                stringBuilder.Append($"_Total Entries:_ {user.Statistics.Anime.Count.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"_Episodes Watched:_ {user.Statistics.Anime.EpisodesWatched.ToString("N0", CultureInfo.InvariantCulture)}\n");
                var t = TimeSpan.FromMinutes(user.Statistics.Anime.MinutesWatched);
                stringBuilder.Append($"_Time Watched:_ {t.Days:00} Days - {t.Hours:00} Hours - {t.Minutes:00} Minutes\n");
                stringBuilder.Append($"_Mean Score:_ {user.Statistics.Anime.MeanScore.ToString("N2", CultureInfo.InvariantCulture)}\n");
            }

            if (withManga)
            {
                stringBuilder.Append($"\n[**Manga List**]({user.SiteUrl}/mangalist)\n");
                stringBuilder.Append($"`Total Entries` {user.Statistics.Manga.Count.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Volumes Read` {user.Statistics.Manga.VolumesRead.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Chapters Read` {user.Statistics.Manga.ChaptersRead.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Mean Score` {user.Statistics.Manga.MeanScore.ToString("N2", CultureInfo.InvariantCulture)}\n");
            }

            embedBuilder.WithDescription(stringBuilder.ToString());

            // Add all extra properties.
            embedBuilder.WithColor(Color.DarkPurple)
                // .WithCurrentTimestamp()
                .WithImageUrl(user.BannerImage)
                .WithThumbnailUrl(user.Avatar.Large)
                .WithTitle($"{user.Name} AniList Statistics")
                .WithUrl(user.SiteUrl);

            return embedBuilder.Build();
        }
    }
}

using Discord;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using YukinaBot.Enums;
using YukinaBot.Models;

namespace YukinaBot.Utility
{
    public class EmbedUtility
    {
        public Embed BuildAnilistMediaEmbed(Media? media)
        {
            var embedBuilder = new EmbedBuilder();

            // First row.
            embedBuilder.WithTitle(media.Title.English ?? media.Title.Romaji)
                .AddField("**Type**", media.Type, true)
                .AddField("**Anilist Score**", media.MeanScore, true)
                .AddField("**Popularity**", media.Popularity, true);

            // Second row.
            embedBuilder.AddField("**Status**", media.Status, true);

            // Second part of second row differs for Anime and Manga.
            if (media.Type == MediaType.Anime)
                embedBuilder.AddField("**Episodes**", media.Episodes != null ? $"{media.Episodes}" : "?", true)
                    .AddField("**Duration**", $"{media.Duration} minutes per episode", true);
            else
                embedBuilder.AddField("**Volumes**", media.Volumes != null ? $"{media.Volumes}" : "?", true)
                    .AddField("**Chapters**", media.Chapters != null ? $"{media.Chapters}" : "?", true);

            // Fourth row.
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("`");
            stringBuilder.Append(string.Join("` - `", media.Genres));
            stringBuilder.Append("`");
            embedBuilder.AddField("**Genres**", stringBuilder.ToString());

            // Add all extra properties.
            embedBuilder.WithColor(Color.Green)
                .WithCurrentTimestamp()
                // Remove all the HTML elements from the description.
                .WithDescription($"_{Regex.Replace(media.Description, "(<\\/?\\w+>)", " ")}_")
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
                stringBuilder.Append("\n**Anime Statistics**\n");
                stringBuilder.Append($"`Total Entries` {user.Statistics.Anime.Count.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Episodes Watched` {user.Statistics.Anime.EpisodesWatched.ToString("N0", CultureInfo.InvariantCulture)}\n");
                TimeSpan t = TimeSpan.FromMinutes(user.Statistics.Anime.MinutesWatched);
                stringBuilder.Append($"`Time Watched` {t.Days:00} Days - {t.Hours:00} Hours - {t.Minutes:00} Minutes\n");
                stringBuilder.Append($"`Mean Score` {user.Statistics.Anime.MeanScore.ToString("N2", CultureInfo.InvariantCulture)}\n");
            }

            if (withManga)
            {
                stringBuilder.Append("\n**Manga Statistics**\n");
                stringBuilder.Append($"`Total Entries` {user.Statistics.Manga.Count.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Volumes Read` {user.Statistics.Manga.VolumesRead.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Chapters Read` {user.Statistics.Manga.ChaptersRead.ToString("N0", CultureInfo.InvariantCulture)}\n");
                stringBuilder.Append($"`Mean Score` {user.Statistics.Manga.MeanScore.ToString("N2", CultureInfo.InvariantCulture)}\n");
            }

            embedBuilder.WithDescription(stringBuilder.ToString());

            // Add all extra properties.
            embedBuilder.WithColor(Color.DarkPurple)
                .WithCurrentTimestamp()
                .WithImageUrl(user.BannerImage)
                .WithThumbnailUrl(user.Avatar.Large)
                .WithTitle(user.Name)
                .WithUrl(user.SiteUrl);

            return embedBuilder.Build();
        }
    }
}

using Discord;
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

            // Second part of second row differs for anime and manga.
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
    }
}

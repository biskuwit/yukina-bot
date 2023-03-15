namespace YukinaBot.Models.AniList
{
    public class Studio
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool? IsAnimationStudio { get; set; }
        public MediaConnection? Media { get; set; }
        public string? SiteUrl { get; set; }
        public int? Favorites { get; set; }
    }
}
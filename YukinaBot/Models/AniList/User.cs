namespace YukinaBot.Models.AniList
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public UserAvatar? Avatar { get; set; }
        public string BannerImage { get; set; } = string.Empty;
        public UserStatisticTypes Statistics { get; set; } = new();
        public string SiteUrl { get; set; } = string.Empty;
        public int UpdatedAt { get; set; }
    }
}

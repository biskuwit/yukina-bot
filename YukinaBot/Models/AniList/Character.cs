namespace YukinaBot.Models.AniList
{
    public class Character
    {
        public int? Id { get; set; }
        public CharacterName? Name { get; set; }
        public CharacterImage? Image { get; set; }
        public string? Description { get; set; }
        public string? SiteUrl { get; set; }
        public MediaConnection? Media { get; set; }
        public int? Favorites { get; set; }
    }
}

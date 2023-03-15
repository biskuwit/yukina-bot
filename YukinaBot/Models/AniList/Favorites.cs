namespace YukinaBot.Models.AniList
{
    public class Favorites
    {
        public MediaConnection? Anime { get; set; }
        public MediaConnection? Manga { get; set; }
        public CharacterConnection? Characters { get; set; }
        public StaffConnection? Staff { get; set; }
        public StudioConnection? Studios { get; set; }
    }
}

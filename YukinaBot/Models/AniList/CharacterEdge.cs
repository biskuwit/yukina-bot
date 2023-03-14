using YukinaBot.Enums.AniList;

namespace YukinaBot.Models.AniList
{
    public class CharacterEdge
    {
        public Character node { get; set; }
        public int id { get; set; }
        public CharacterRole role { get; set; }
        public List<Staff> voiceActors { get; set; }
        public List<Media> media { get; set; }
        public int favouriteOrder { get; set; }
    }
}

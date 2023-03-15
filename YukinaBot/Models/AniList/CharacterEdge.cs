using YukinaBot.Enums.AniList;

namespace YukinaBot.Models.AniList
{
    public class CharacterEdge
    {
        public Character? Node { get; set; }
        public int? Id { get; set; }
        public CharacterRole? Role { get; set; }
        public List<Staff>? VoiceActors { get; set; }
        public List<Media>? Media { get; set; }
        public int? FavoriteOrder { get; set; }
    }
}

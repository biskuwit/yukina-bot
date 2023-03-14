using YukinaBot.Enums.AniList;

namespace YukinaBot.Models.AniList
{
    public class MediaEdge
    {
        public Media node { get; set; }
        public int id { get; set; }
        public MediaRelation relationType { get; set; }
        public bool isMainStudio { get; set; }
        public List<Character> characters { get; set; }
        public CharacterRole characterRole { get; set; }
        public string staffRole { get; set; }
        public List<Staff> voiceActors { get; set; }
        public int favouriteOrder { get; set; }
    }
}

namespace YukinaBot.Models.AniList
{
    public class CharacterConnection
    {
        public List<CharacterEdge> edges { get; set; }
        public List<Character> nodes { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}

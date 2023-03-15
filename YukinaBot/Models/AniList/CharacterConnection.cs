namespace YukinaBot.Models.AniList
{
    public class CharacterConnection
    {
        public List<CharacterEdge>? Edges { get; set; }
        public List<Character>? Nodes { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}

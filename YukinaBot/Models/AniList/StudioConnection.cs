namespace YukinaBot.Models.AniList
{
    public class StudioConnection
    {
        public List<StudioEdge>? Edges { get; set; }
        public List<Studio>? Nodes { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}

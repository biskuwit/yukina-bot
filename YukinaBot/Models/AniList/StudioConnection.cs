namespace YukinaBot.Models.AniList
{
    public class StudioConnection
    {
        public List<StudioEdge> edges { get; set; }
        public List<Studio> nodes { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}

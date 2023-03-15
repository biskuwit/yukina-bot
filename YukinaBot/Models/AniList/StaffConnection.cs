namespace YukinaBot.Models.AniList
{
    public class StaffConnection
    {
        public List<StaffEdge>? Edges { get; set; }
        public List<Staff>? Nodes { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}

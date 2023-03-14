namespace YukinaBot.Models.AniList
{
    public class StaffConnection
    {
        public List<StaffEdge> edges { get; set; }
        public List<Staff> nodes { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}

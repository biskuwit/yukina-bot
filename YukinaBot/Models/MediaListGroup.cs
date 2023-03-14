using YukinaBot.Enums;

namespace YukinaBot.Models
{
    public class MediaListGroup
    {
        public List<MediaList>? Entries { get; set; }
        public string? Name { get; set; }
        public MediaListStatus Status { get; set; }
    }
}

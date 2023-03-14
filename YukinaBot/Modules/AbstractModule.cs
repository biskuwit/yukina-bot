using Discord.Commands;
using YukinaBot.Services;
using YukinaBot.Utility;

namespace YukinaBot.Modules
{
    public abstract class AbstractModule : ModuleBase<SocketCommandContext>
    {
        public AniListFetcher AniListFetcher = new();
        public EmbedUtility EmbedUtility = new();
    }
}

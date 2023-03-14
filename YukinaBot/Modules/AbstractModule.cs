using Discord.Commands;
using YukinaBot.Services;
using YukinaBot.Utility;

namespace YukinaBot.Modules
{
    public abstract class AbstractModule : ModuleBase<SocketCommandContext>
    {
        protected AniListFetcher AniListFetcher = new();
        protected EmbedUtility EmbedUtility = new();
    }
}

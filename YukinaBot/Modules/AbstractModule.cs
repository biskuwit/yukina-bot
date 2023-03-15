using Discord.Commands;
using MongoDB.Driver;
using YukinaBot.Helpers;
using YukinaBot.Services;
using YukinaBot.Utility;

namespace YukinaBot.Modules
{
    public abstract class AbstractModule : ModuleBase<SocketCommandContext>
    {
        protected AniListFetcher AniListFetcher = new();
        protected EmbedUtility EmbedUtility = new();
        protected LevenshteinUtility LevenshteinUtility = new();
        protected MongoDbUtility MongoDbUtility = new();
        protected MongoClient MongoClient = new(ConfigurationHelper.GetMongoDbConnectionUri());
    }
}

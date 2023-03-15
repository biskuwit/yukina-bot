using Discord;

namespace YukinaBot.Utility
{
    public class MongoDbUtility
    {
        public IUser FindDiscordUserInChannel(IChannel channel, ulong userId)
        {
            return channel.GetUserAsync(userId).Result;
        }
    }
}

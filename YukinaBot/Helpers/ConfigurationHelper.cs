namespace YukinaBot.Helpers
{
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Get your Discord Bot Token from Environment Variable
        /// </summary>
        /// <returns>Token</returns>
        public static string? GetToken()
        {
            return Environment.GetEnvironmentVariable("YUKINA_BOT_TOKEN");
        }

        /// <summary>
        /// Get your Debugging Guild ID from Environment Variable
        /// </summary>
        /// <returns></returns>
        public static string? GetDebugGuildId()
        {
            return Environment.GetEnvironmentVariable("YUKINA_DEBUG_GUILD_ID");
        }

        /// <summary>
        /// Get the MongoDB Connection URI from Environment Variable
        /// </summary>
        /// <returns></returns>
        public static string? GetMongoDbConnectionUri()
        {
            return Environment.GetEnvironmentVariable("YUKINA_DB_CONNECTION");
        }

        /// <summary>'
        /// Get the Bot Prefix from Environment Variable
        /// </summary>
        /// <returns></returns>
        public static string? GetBotPrefix()
        {
            return Environment.GetEnvironmentVariable("YUKINA_BOT_PREFIX");
        }
    }
}

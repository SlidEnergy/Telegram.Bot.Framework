namespace Telegram.Bot.Framework.Abstractions
{
    /// <summary>
    /// Configurations for the bot.
    /// </summary>
    public interface IBotOptions
    {
        /// <summary>
        /// Username of the bot.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Optional if client not needed. Telegram API token.
        /// </summary>
        string ApiToken { get; set; }

        /// <summary>
        /// URL path to webhook address.
        /// </summary>
        string WebhookPath { get; set; }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extenstion methods for adding Telegram Bot framework to the ASP.NET Core middleware
    /// </summary>
    public static class TelegramBotMiddlewareExtensions
    {
        /// <summary>
        /// Add Telegram bot webhook handling functionality to the pipeline
        /// </summary>
        /// <typeparam name="TBot">Type of bot</typeparam>
        /// <param name="app">Instance of IApplicationBuilder</param>
        /// <param name="botBuilder">Instance of <see cref="IBotBuilder"/></param>
        /// <returns>Instance of IApplicationBuilder</returns>
        public static IApplicationBuilder UseTelegramBotWebhook<TBot>(
            this IApplicationBuilder app,
            IBotBuilder botBuilder
        )
            where TBot : BotBase
        {
            var updateDelegate = botBuilder.Build();

            var options = app.ApplicationServices.GetRequiredService<IOptions<BotOptions<TBot>>>();
            app.Map(
                options.Value.WebhookPath,
                builder => builder.UseMiddleware<TelegramBotMiddleware<TBot>>(updateDelegate)
            );

            return app;
        }
    }
}
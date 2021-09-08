using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        public static IApplicationBuilder UseTelegramBotLongPolling<TBot>(this IApplicationBuilder app,
            IBotBuilder botBuilder,
            TimeSpan startAfter = default,
            CancellationToken cancellationToken = default) where TBot : BotBase
        {
            if (startAfter == default)
            {
                startAfter = TimeSpan.FromSeconds(2);
            }

            var serviceProvider = new BotServiceProvider(app);
            var updateManager = new UpdatePollingManager<TBot>(botBuilder, serviceProvider);

            Task.Run(async () =>
            {
                await Task.Delay(startAfter, cancellationToken);
                await updateManager.RunAsync(cancellationToken: cancellationToken);
            }, cancellationToken)
            .ContinueWith(t =>
            {
                if (t.Exception == null)
                    return;

                var logger = serviceProvider.GetService(typeof(ILogger<BotBase>)) as ILogger<BotBase>;
                logger?.LogError("Thrown exception in UseTelegramBotLongPolling(): {Exception}", t.Exception);
                throw t.Exception;
            }, TaskContinuationOptions.OnlyOnFaulted);

            return app;
        }

        public static IApplicationBuilder EnsureWebhookSet<TBot>(this IApplicationBuilder app)
            where TBot : IBot
        {
            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            var bot = scope.ServiceProvider.GetRequiredService<TBot>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<BotOptions<TBot>>>();
            var url = new Uri(options.Value.WebhookPath);

            logger?.LogInformation("Setting webhook for bot \"{Name}\" to URL \"{Url}\"", typeof(TBot).Name, url);

            bot.Client.SetWebhookAsync(url.AbsoluteUri)
                .GetAwaiter().GetResult();

            return app;
        }
    }
}
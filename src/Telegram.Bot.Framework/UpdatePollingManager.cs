using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Framework
{
    /// <summary>
    /// Update poll manager.
    /// </summary>
    /// <typeparam name="TBot"></typeparam>
    public class UpdatePollingManager<TBot> : IUpdatePollingManager<TBot>
             where TBot : IBot
    {
        private readonly UpdateDelegate _updateDelegate;
        private readonly IBotServiceProvider _rootProvider;

        public UpdatePollingManager(
            IBotBuilder botBuilder,
            IBotServiceProvider rootProvider)
        {
            _updateDelegate = botBuilder.Build();
            _rootProvider = rootProvider;
        }

        public UpdatePollingManager(
            UpdateDelegate updateDelegate,
            IBotServiceProvider rootProvider)
        {
            _updateDelegate = updateDelegate;
            _rootProvider = rootProvider;
        }

        public async Task RunAsync(
            GetUpdatesRequest requestParams = default,
            CancellationToken cancellationToken = default)
        {
            var bot = (TBot)_rootProvider.GetService(typeof(TBot));

            await bot.Client.DeleteWebhook(true, cancellationToken)
                .ConfigureAwait(false);

            requestParams ??= new GetUpdatesRequest
            {
                Offset = 0,
                Timeout = 500,
                AllowedUpdates = Array.Empty<UpdateType>(),
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await bot.Client.GetUpdates(
                    offset: requestParams.Offset,
                    timeout: requestParams.Timeout,
                    allowedUpdates: requestParams.AllowedUpdates,
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);

                foreach (var update in updates)
                {
                    using var scopeProvider = _rootProvider.CreateScope();
                    var context = new UpdateContext(bot, update, scopeProvider);
                    // ToDo deep clone bot instance for each update
                    await _updateDelegate(context)
                        .ConfigureAwait(false);
                }

                if (updates.Length > 0)
                {
                    requestParams.Offset = updates[^1].Id + 1;
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
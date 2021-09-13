using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Extensions;

namespace Quickstart.AspNetCore.Handlers
{
    public class CallbackQueryHandler : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsCallbackQueryUpdate();

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var cq = context.Update.CallbackQuery;
            await context.Bot.Client.AnswerCallbackQueryAsync(cq.Id, "PONG", true);
            await next(context);
        }
    }
}
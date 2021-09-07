using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class CallbackQueryHandler : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => When.CallbackQuery(context);

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var cq = context.Update.CallbackQuery;
            await context.Bot.Client.AnswerCallbackQueryAsync(cq.Id, "PONG", true);
            await next(context);
        }
    }
}
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class TextEchoer : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => When.NewTextMessage(context);

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;

            await context.Bot.Client.SendTextMessageAsync(
                msg.Chat, "You said:\n" + msg.Text
            );

            await next(context);
        }
    }
}
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Extensions;

namespace Quickstart.AspNetCore.Handlers
{
    public class TextFilter : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsTextMessageUpdate() &&
                                                                  context.GetTextMessage().ToLower().Contains("fuck");

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;

            await context.Bot.Client.SendTextMessageAsync(
                msg.Chat, "You said abuse word:\n" + msg.Text
            );
        }
    }
}
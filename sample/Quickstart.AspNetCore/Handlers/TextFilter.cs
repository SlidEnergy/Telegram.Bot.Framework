using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class TextFilter : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsTextMessageUpdate() &&
                                                                  context.GetTextMessage().ToLower().Contains("fuck");

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;

            await context.Bot.Client.SendMessage(
                msg.Chat, "You said abuse word:\n" + msg.Text
            );
        }
    }
}
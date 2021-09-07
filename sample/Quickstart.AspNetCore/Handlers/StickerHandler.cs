using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class StickerHandler : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => When.StickerMessage(context);

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;
            var incomingSticker = msg.Sticker;

            var evilMindsSet = await context.Bot.Client.GetStickerSetAsync("EvilMinds");

            var similarEvilMindSticker = evilMindsSet.Stickers.FirstOrDefault(
                sticker => incomingSticker.Emoji.Contains(sticker.Emoji)
            );

            var replySticker = similarEvilMindSticker ?? evilMindsSet.Stickers.First();

            await context.Bot.Client.SendStickerAsync(
                msg.Chat,
                replySticker.FileId,
                replyToMessageId: msg.MessageId
            );
        }
    }
}

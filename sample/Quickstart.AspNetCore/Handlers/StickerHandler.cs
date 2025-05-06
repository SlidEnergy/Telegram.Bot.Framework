using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class StickerHandler : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsStickerMessageUpdate();

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;
            var incomingSticker = msg.Sticker;

            var evilMindsSet = await context.Bot.Client.GetStickerSet("EvilMinds");

            var similarEvilMindSticker = evilMindsSet.Stickers.FirstOrDefault(
                sticker => incomingSticker.Emoji.Contains(sticker.Emoji)
            );

            var replySticker = similarEvilMindSticker ?? evilMindsSet.Stickers.First();

            await context.Bot.Client.SendSticker(
                msg.Chat,
                replySticker.FileId,
                replyParameters: new Telegram.Bot.Types.ReplyParameters { MessageId = msg.MessageId }
            );
        }
    }
}

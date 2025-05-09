﻿using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class TextEchoer : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsTextMessageUpdate();

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;

            await context.Bot.Client.SendMessage(
                msg.Chat, "You said:\n" + msg.Text
            );

            await next(context);
        }
    }
}
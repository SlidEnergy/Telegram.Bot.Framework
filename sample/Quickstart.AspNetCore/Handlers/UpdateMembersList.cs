using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Extensions;

namespace Quickstart.AspNetCore.Handlers
{
    public class UpdateMembersList : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => context.IsMembersChangedUpdate();

        public override Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Updating chat members list...");
            Console.ResetColor();

            return next(context);
        }
    }
}

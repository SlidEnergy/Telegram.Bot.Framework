using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class UpdateMembersList : UpdateHandlerBase
    {
        public override bool CanHandle(IUpdateContext context) => When.MembersChanged(context);

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

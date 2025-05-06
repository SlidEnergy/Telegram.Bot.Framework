using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Framework.Abstractions;

namespace Quickstart.AspNetCore.Handlers
{
    public class StartCommand : CommandBase
    {
        public StartCommand() : base("start")
        {
        }
        
        protected override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
        {
            await context.Bot.Client.SendMessage(context.Update.Message.Chat, "Hello, World!");
        }
    }
}

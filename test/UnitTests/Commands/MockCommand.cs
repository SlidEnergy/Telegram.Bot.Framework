using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace UnitTests.Commands
{
    public class MockCommand : CommandBase
    {
        public MockCommand(string commandName) : base(commandName)
        {
        }

        protected override Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
        {
            return Task.CompletedTask;
        }
    }
}
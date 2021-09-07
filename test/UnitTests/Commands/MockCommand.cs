using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace UnitTests.Commands
{
    public class MockCommand : CommandBase
    {
        private readonly Func<IUpdateContext, UpdateDelegate, string[], Task> _handler;

        public MockCommand(Func<IUpdateContext, UpdateDelegate, string[], Task> handler) : base("mock")
        {
            _handler = handler;
        }

        protected override Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
            => _handler(context, next, args);
    }
}
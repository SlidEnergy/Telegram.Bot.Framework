using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;

namespace Quickstart.AspNetCore
{
    public class EchoBot : BotBase
    {
        public EchoBot(IOptions<BotOptions> options)
            : base(options.Value)
        {
        }
    }
}

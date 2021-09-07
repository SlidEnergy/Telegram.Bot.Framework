using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace Telegram.Bot.Framework
{
    public class BotBuilder : IBotBuilder
    {
        internal UpdateDelegate UpdateDelegate { get; private set; }
        private readonly ICollection<Func<UpdateDelegate, UpdateDelegate>> _components;

        public BotBuilder()
        {
            _components = new List<Func<UpdateDelegate, UpdateDelegate>>();
        }

        public IBotBuilder Use<THandler>()
            where THandler : IUpdateHandler
        {
            _components.Add(next =>
            {
                return context =>
                {
                    var handler = (IUpdateHandler)context.Services.GetService(typeof(THandler));
                    return handler.CanHandle(context) ? 
                        handler.HandleAsync(context, next) : next(context);
                };
            });

            return this;
        }

        public IBotBuilder Use<THandler>(THandler handler)
            where THandler : IUpdateHandler
        {
            _components.Add(next =>
            {
                return context => handler.CanHandle(context) ? 
                    handler.HandleAsync(context, next) : next(context);
            });

            return this;
        }

        public UpdateDelegate Build()
        {
            UpdateDelegate handle = context =>
            {
                // use Logger
                Console.WriteLine("No handler for update {0} of type {1}.", context.Update.Id, context.Update.Type);
                return Task.FromResult(1);
            };

            handle = _components.Reverse()
                .Aggregate(handle, (current, component) => component(current));

            return UpdateDelegate = handle;
        }
    }
}

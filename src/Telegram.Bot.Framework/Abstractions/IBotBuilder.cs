﻿namespace Telegram.Bot.Framework.Abstractions
{
    public interface IBotBuilder
    {
        IBotBuilder Use<THandler>()
            where THandler : IUpdateHandler;

        IBotBuilder Use<THandler>(THandler handler)
            where THandler : IUpdateHandler;

        UpdateDelegate Build();
    }
}

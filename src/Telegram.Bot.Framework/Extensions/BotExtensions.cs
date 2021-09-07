﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Framework
{
    public static class BotExtensions
    {
        public static bool CanHandleCommand(this IBot bot, string commandName, Message message)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentException("Invalid command name", nameof(commandName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (commandName.StartsWith("/"))
                throw new ArgumentException("Command name must not start with '/'.", nameof(commandName));

            {
                var isTextMessage = message.Text != null;
                if (!isTextMessage)
                    return false;
            }

            {
                var isCommand = message.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;
                if (!isCommand)
                    return false;
            }

            return Regex.IsMatch(
                message.EntityValues.First(),
                $@"^/{commandName}(?:@{bot.Username})?$",
                RegexOptions.IgnoreCase
            );
        }
    }
}
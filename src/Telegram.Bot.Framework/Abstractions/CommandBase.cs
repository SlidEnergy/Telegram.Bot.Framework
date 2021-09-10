using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Framework.Abstractions
{
    /// <summary>
    /// Base handler implementation for a commandName such as "/start"
    /// </summary>
    public abstract class CommandBase : IUpdateHandler
    {
        private readonly string _commandName;

        protected CommandBase(string commandName)
        {
            if (commandName.StartsWith("/"))
                throw new ArgumentException("Command name must not start with '/'.", nameof(commandName));
            
            _commandName = commandName;
        }

        public virtual bool CanHandle(IUpdateContext context)
        {
            var message = context.Update?.Message;
            
            if (message == null)
                return false;
            
            if (string.IsNullOrWhiteSpace(message.Text))
                return false;

            var isCommand = message.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;
            if (!isCommand)
                return false;

            var isCommandFromGroup = Regex.IsMatch(
                message.EntityValues.First(),
                $@"^/{_commandName}(?:@{context.Bot.Username})?$",
                RegexOptions.IgnoreCase);

            if (!isCommandFromGroup)
                return false;

            return true;
        }

        protected abstract Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args);

        public Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            return CanHandle(context) ?
                HandleAsync(context, next, ParseCommandArgs(context.Update.Message))
                : next(context);
        }

        public static string[] ParseCommandArgs(Message message)
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));
            if (message.Entities?.FirstOrDefault()?.Type != MessageEntityType.BotCommand)
                throw new ArgumentException("Message is not a commandName", nameof(message));

            var argsList = new List<string>();
            var allArgs = message.Text[message.Entities[0].Length..].TrimStart();
            argsList.Add(allArgs);

            var expandedArgs = Regex.Split(allArgs, @"\s+");
            if (expandedArgs.Length > 1)
            {
                argsList.AddRange(expandedArgs);
            }

            var args = argsList
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            return args;
        }
    }
}
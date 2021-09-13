using System.Linq;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Framework.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IUpdateContext"/>
    /// </summary>
    public static class UpdateContextExtensions
    {
        /// <summary>
        /// Determines if update is a webhook or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains webhook.</returns>
        public static bool IsWebhookUpdate(this IUpdateContext context)
            => context.Items.ContainsKey(nameof(HttpContext));

        /// <summary>
        /// Determines if update is a message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains message.</returns>
        public static bool IsMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message != null;

        /// <summary>
        /// Determines if update is a text message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains text message.</returns>
        public static bool IsTextMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message?.Text != null;
        
        /// <summary>
        /// Determines if update is a reply message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains reply message.</returns>
        public static bool IsReplyMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message?.ReplyToMessage != null;
        
        /// <summary>
        /// Determines if update is a reply text message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains reply text message.</returns>
        public static bool IsReplyTextMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message?.ReplyToMessage?.Text != null;

        /// <summary>
        /// Determines if update is command or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains command entity.</returns>
        public static bool IsCommandUpdate(this IUpdateContext context) =>
            context.Update?.Message?.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;

        /// <summary>
        /// Determines if update is member changed entity or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains member changed entity.</returns>
        public static bool IsMembersChangedUpdate(this IUpdateContext context) =>
            context.Update?.Message?.NewChatMembers != null ||
            context.Update?.Message?.LeftChatMember != null ||
            context.Update?.ChannelPost?.NewChatMembers != null ||
            context.Update?.ChannelPost?.LeftChatMember != null;

        /// <summary>
        /// Determines if update is a location message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains location message.</returns>
        public static bool IsLocationMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message?.Location != null;

        /// <summary>
        /// Determines if update is a sticker message or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains sticker message.</returns>
        public static bool IsStickerMessageUpdate(this IUpdateContext context) =>
            context.Update?.Message?.Sticker != null;

        /// <summary>
        /// Determines if update is a callback query or not.
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <returns>True if update contains callback query.</returns>
        public static bool IsCallbackQueryUpdate(this IUpdateContext context) =>
            context.Update?.CallbackQuery != null;
    }
}
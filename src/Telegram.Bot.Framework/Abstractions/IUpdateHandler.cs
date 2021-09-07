using System.Threading.Tasks;

namespace Telegram.Bot.Framework.Abstractions
{
    /// <summary>
    /// Processes an update
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Handles the update for bot. This method will be called only if CanHandleUpdate returns <value>true</value>
        /// </summary>
        /// <param name="context">Instance of <see cref="IUpdateContext"/></param>
        /// <param name="next">Next update delegate</param>
        /// <returns>Result of handling this update</returns>
        Task HandleAsync(IUpdateContext context, UpdateDelegate next);
    }
}
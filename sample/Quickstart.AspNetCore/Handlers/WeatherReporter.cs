using Quickstart.AspNetCore.Services;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace Quickstart.AspNetCore.Handlers
{
    public class WeatherReporter : UpdateHandlerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherReporter(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public override bool CanHandle(IUpdateContext context) => When.LocationMessage(context);

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next)
        {
            var msg = context.Update.Message;
            var location = msg.Location;

            var weather = await _weatherService.GetWeatherAsync(location.Latitude, location.Longitude);

            await context.Bot.Client.SendTextMessageAsync(
                msg.Chat,
                $"Weather status is *{weather.Status}* with the temperature of {weather.Temp:F1}.\n" +
                $"Min: {weather.MinTemp:F1}\n" +
                $"Max: {weather.MaxTemp:F1}\n\n\n" +
                "powered by [MetaWeather](https://www.metaweather.com)",
                ParseMode.Markdown,
                replyToMessageId: msg.MessageId
            );
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Framework;
using Quickstart.AspNetCore.Services;

namespace Quickstart.AspNetCore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var botOptions = Configuration.GetSection("EchoBot").Get<BotOptions>();
            services.AddTelegramBot<EchoBot>(botOptions);
            services.AddScoped<IWeatherService, WeatherService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // get bot updates from Telegram via long-polling approach during development
                // this will disable Telegram webhooks
                app.UseTelegramBotLongPolling<EchoBot>(TimeSpan.FromSeconds(2));
            }
            else
            {
                // use Telegram bot webhook middleware in higher environments
                app.UseTelegramBotWebhook<EchoBot>();
                // and make sure webhook is enabled
                app.EnsureWebhookSet<EchoBot>();
            }

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}

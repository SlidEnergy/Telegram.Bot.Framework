# Extended Telegram Bot Framework for .NET

[![NuGet](https://img.shields.io/nuget/v/Telegram.Bot.MagicFramework)](https://www.nuget.org/packages/Telegram.Bot.MagicFramework)
[![License](https://img.shields.io/github/license/suxrobgm/Telegram.Bot.Framework)](https://github.com/suxrobGM/Telegram.Bot.Framework/raw/master/LICENSE)

<img src="./docs/icon.png" alt="Telegram Bot Framework Logo" width=200 height=200 />

Simple framework for building Telegram bots 🤖. Ideal for running multiple chat bots inside a single ASP.NET Core app.

See some **sample bots** in action:

- Echo bot:   [`@Sample_Echoer_Bot`](https://t.me/sample_echoer_bot)
- Games bot:  [`@CrazyCircleBot`](https://t.me/CrazyCircleBot)

## Getting Started
This project redesigned version of old [Telegram.Bot.Framework](https://github.com/TelegramBots/Telegram.Bot.Framework)

Creating a bot with good architecture becomes very simple using this framework. Have a look at the [**Quick Start** wiki](./docs/wiki/quick-start/echo-bot.md) to make your fist _Echo Bot_.

There is much more you can do with your bot. See what's available at [**wikis**](./docs/wiki/README.md).

## Framework Features

- Allows you to have multiple bots running inside one app
- Able to share code(update handlers) between multiple bots
- Easy to use with webhooks(specially with Docker deployments)
- Optimized for making Telegram Games
- Simplifies many repititive tasks in developing bots

## Breaking changes comparing with [Telegram.Bot.Framework](https://github.com/TelegramBots/Telegram.Bot.Framework)
- No longer support .NET Framework
- Supported platforms: `.netstandart 2.1`, `.netcore 3.1` and higher
- Support new version of [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)
- Redesigned update handler pipeline

## Samples

Don't wanna read wikis? Read C# code of sample projects in [samples directory](./sample/).

using Moq;
using System;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace UnitTests
{
    public class CommandHandling
    {
        [Theory(DisplayName = "Should accept handling valid \"/test\" commands for bot \"@Test_bot\"")]
        [InlineData("/test", "/test")]
        [InlineData("/test    ", "/test")]
        [InlineData("/test abc", "/test")]
        [InlineData("/TesT", "/tESt")]
        [InlineData("/test@test_bot", "/test@test_bot")]
        [InlineData("/test@test_bot ", "/test@test_bot")]
        [InlineData("/test@test_bot  !", "/test@test_bot")]
        public void Should_Parse_Valid_Commands(string text, string commandValue)
        {
            var mockBot = new Mock<IBot>();
            mockBot.SetupGet(x => x.Username).Returns("Test_Bot");

            var bot = mockBot.Object;
            var message = new Message
            {
                Text = text,
                Entities = new[]
                {
                    new MessageEntity
                    {
                        Type = MessageEntityType.BotCommand,
                        Offset = text.IndexOf(commandValue, StringComparison.OrdinalIgnoreCase),
                        Length = commandValue.Length
                    },
                },
            };

            var result = bot.CanHandleCommand("test", message);

            Assert.True(result);
        }

        [Theory(DisplayName = "Should reject parsing non-command text messages as command \"/test\"")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("/\t")]
        [InlineData("    ")]
        [InlineData("I AM NOT A COMMAND")]
        [InlineData("/testt")]
        [InlineData("/@test_bot")]
        [InlineData("/tes@test_bot")]
        public void Should_Not_Parse_Invalid_Command_Text(string text)
        {
            var mockBot = new Mock<IBot>();
            mockBot.SetupGet(x => x.Username).Returns("Test_Bot");

            var bot = mockBot.Object;
            var message = new Message
            {
                Text = text,
            };

            var result = bot.CanHandleCommand("test", message);

            Assert.False(result);
        }

        [Fact(DisplayName = "Should not accept handling non-text messages")]
        public void Should_Refuse_Handling_Non_Message_Updates()
        {
            var mockBot = new Mock<IBot>();
            mockBot.SetupGet(x => x.Username).Returns("Test_Bot");

            var bot = mockBot.Object;
            var message = new Message
            {
                Text = null,
            };

            var result = bot.CanHandleCommand("test", message);

            Assert.False(result);
        }
    }
}
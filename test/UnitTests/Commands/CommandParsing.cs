﻿using System;
using Newtonsoft.Json;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Xunit;

namespace UnitTests.Commands
{
    public class ArgumentParsingTests
    {
        [Theory(DisplayName = "Should parse valid single command in a message")]
        [InlineData("/start")]
        [InlineData("/_")]
        [InlineData("/2")]
        [InlineData("/3_")]
        [InlineData("/start@ab3BOT")]
        [InlineData("/1@N_BoT")]
        public void Should_Parse_Valid_Commands(string command)
        {
            var message = JsonConvert.DeserializeObject<Message>($@"{{
                message_id: 2,
                chat: {{ id: 333, type: ""private"" }},
                from: {{ id: 333, first_name: ""Poulad"", is_bot: false }},
                text: {JsonConvert.SerializeObject(command)},
                entities: [ {{ offset: 0, length: {command.Length}, type: ""bot_command"" }} ],
                date: 1000
            }}");

            var args = CommandBase.ParseCommandArgs(message);

            Assert.Empty(args);
        }

        [Theory(DisplayName = "Should parse valid a command with its arguments")]
        [InlineData("/1 bar", 2, "bar")]
        [InlineData("/foo bar baz", 4, "bar baz", "bar", "baz")]
        [InlineData("/foo\tbar baz", 4, "bar baz", "bar", "baz")]
        [InlineData("/foo\nbar baz", 4, "bar baz", "bar", "baz")]
        [InlineData("/_@9", 2, "@9")]
        [InlineData("/1-5 ab cd", 2, "-5 ab cd", "-5", "ab", "cd")]
        [InlineData("/1@T_Bot arg_1", 8, "arg_1")]
        [InlineData("/1@T_Bot arg_1 \"test\"", 8, "arg_1 \"test\"", "arg_1", @"""test""")]
        public void Should_Parse_Valid_Commands2(string text, int commandLength, params string[] expectedArgs)
        {
            var message = JsonConvert.DeserializeObject<Message>($@"{{
                message_id: 2,
                chat: {{ id: 333, type: ""private"" }},
                from: {{ id: 333, first_name: ""Poulad"", is_bot: false }},
                text: {JsonConvert.SerializeObject(text)},
                entities: [ {{ offset: 0, length: {commandLength}, type: ""bot_command"" }} ],
                date: 1000
            }}");

            var args = CommandBase.ParseCommandArgs(message);

            Assert.Equal(expectedArgs, args);
        }

        [Fact(DisplayName = "Should throw exception if the message is null")]
        public void Should_Throw_When_Null_Message()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                CommandBase.ParseCommandArgs(null)
            );

            Assert.Equal("Value cannot be null. (Parameter 'message')", e.Message);
            Assert.Equal("message", e.ParamName);
        }

        [Fact(DisplayName = "Should returns empty array if the message does not have any entities")]
        public void No_Message_Entity()
        {
            var message = JsonConvert.DeserializeObject<Message>($@"{{
                message_id: 2,
                chat: {{ id: 333, type: ""private"" }},
                from: {{ id: 333, first_name: ""Poulad"", is_bot: false }},
                date: 1000
            }}");

            var args = CommandBase.ParseCommandArgs(message);
            Assert.Equal(ArraySegment<string>.Empty, args);
        }

        [Fact(DisplayName = "Should returns empty array if the message entities array is empty")]
        public void Empty_Message_Entities()
        {
            var message = JsonConvert.DeserializeObject<Message>($@"{{
                message_id: 2,
                chat: {{ id: 333, type: ""private"" }},
                from: {{ id: 333, first_name: ""Poulad"", is_bot: false }},
                entities: [ ],
                date: 1000
            }}");

            var args = CommandBase.ParseCommandArgs(message);
            Assert.Equal(ArraySegment<string>.Empty, args);
        }

        [Fact(DisplayName = "Should returns empty array if the first message entity is not a command")]
        public void First_Message_Entity_Not_Command()
        {
            var message = JsonConvert.DeserializeObject<Message>($@"{{
                message_id: 2,
                chat: {{ id: 333, type: ""private"" }},
                from: {{ id: 333, first_name: ""Poulad"", is_bot: false }},
                entities: [ {{ offset: 0, length: 4, type: ""bold"" }} ],
                date: 1000
            }}");
            
            var args = CommandBase.ParseCommandArgs(message);
            Assert.Equal(ArraySegment<string>.Empty, args);
        }
    }
}
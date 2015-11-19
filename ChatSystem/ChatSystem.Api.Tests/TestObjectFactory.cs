﻿namespace ChatSystem.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ChatSystem.Models;

    using Moq;

    using Services.Data.Contracts;

    public static class TestObjectFactory
    {
        private static IQueryable<ChatMessage> messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                 Id = 5,
                 Receiver = new User { UserName = "User1" },
                 Sender = new User { UserName = "User5" },
                 SentOn = DateTime.Now
            },
              new ChatMessage
            {
                 Id = 7,
                 Receiver = new User { UserName = "User1" },
                 Sender = new User { UserName = "User5" },
                 SentOn = DateTime.Now
            },
            new ChatMessage
            {
                 Id = 6,
                 Receiver = new User { UserName = "User3" },
                 Sender = new User { UserName = "User4" },
                 SentOn = DateTime.Now
            }
        }.AsQueryable();

        public static IMessagesService GetMessagesService()
        {
            var messagesService = new Mock<IMessagesService>();
            messagesService.Setup(m => m.All(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(messages);
            messagesService.Setup(m => m.GetMessage(5)).Returns(messages.Where(m => m.Id == 5));
            messagesService.Setup(m => m.DeleteMessage(5, "User5")).Returns(true);
            messagesService.Setup(m => m.SetReadToAll("User5", "User1")).Returns(true);
            return messagesService.Object;
        }

        public static IPresenceService GetPresenceService()
        {
            var presenceService = new Mock<IPresenceService>();
            presenceService.Setup(m => m.UpdatePresence("User5"));
            return presenceService.Object;
        }
    }
}

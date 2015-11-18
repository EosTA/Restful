namespace ChatSystem.Api.Tests
{
    using Services.Data.Contracts;
    using Moq;
    using Services.Data;
    using System.Linq;
    using System.Collections.Generic;
    using ChatSystem.Models;
    using System;
    using Controllers;

    public static class TestObjectFactory
    {
        private static IQueryable<ChatMessage> messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                 Id = 5,
                 Receiver = new User {UserName = "User1" },
                 Sender = new User {UserName = "User5" },
                 SentOn = DateTime.Now
            },
              new ChatMessage
            {
                 Id = 7,
                 Receiver = new User {UserName = "User1" },
                 Sender = new User {UserName = "User5" },
                 SentOn = DateTime.Now
            },
            new ChatMessage
            {
                 Id = 6,
                 Receiver = new User {UserName = "User3" },
                 Sender = new User {UserName = "User4" },
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
                It.IsAny<int>()
                ))
                .Returns(messages);

            messagesService.Setup(m => m.DeleteMessage(5, "User5")).Returns(true);
            return messagesService.Object;
        }
    }
}

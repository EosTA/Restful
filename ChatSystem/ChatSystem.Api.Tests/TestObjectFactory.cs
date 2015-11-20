namespace ChatSystem.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data.Models;

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

        public static IQueryable<User> Users
        {
            get
            {
                var users = new List<User>
                            {
                                new User
                                {
                                    Id = "User0",
                                    UserName = "Username0",
                                    FirstName = "User0",
                                    LastName = "User lastName0",
                                    AvatarUrl = "Avatar0"
                                },
                                new User
                                {
                                    Id = "User01",
                                    UserName = "Username01",
                                    FirstName = "User01",
                                    LastName = "User lastName01",
                                    AvatarUrl = "Avatar01"
                                },
                                new User
                                {
                                    Id = "User02",
                                    UserName = "Username02",
                                    FirstName = "User02",
                                    LastName = "User lastName02",
                                    AvatarUrl = "Avatar02"
                                },
                                new User
                                {
                                    Id = "User03",
                                    UserName = "Username03",
                                    FirstName = "User03",
                                    LastName = "User lastName03",
                                    AvatarUrl = "Avatar03"
                                }
                            };

                return users.AsQueryable();
            }
        }

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

        public static IAvatarsService GetAvatarsService()
        {
            var avatarsService = new Mock<IAvatarsService>();
            var urlName = Users.FirstOrDefault().AvatarUrl;
            avatarsService.Setup(a => a.Get(It.IsAny<string>())).Returns(urlName);
            return avatarsService.Object;
        }
    }
}

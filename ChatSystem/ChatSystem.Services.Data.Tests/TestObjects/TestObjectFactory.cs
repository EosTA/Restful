namespace ChatSystem.Services.Data.Tests.TestObjects
{
    using System;
    using ChatSystem.Models;

    public static class TestObjectFactory
    {
        public static InMemoryRepository<ChatMessage> GetMessageRepository(int messages = 20)
        {
            var repository = new InMemoryRepository<ChatMessage>();

            for (int i = 0; i < messages; i++)
            {
                var message = new ChatMessage();
                message.Id = i;
                message.Message = "Test message" + i;
                message.Sender = new User { UserName = "User" + i, LastName = "User lastName" + i };
                message.Receiver = new User { UserName = "User" + i + 1, LastName = "User lastName" + i + 1 };
                message.SentOn = DateTime.Now;
                repository.Add(message);
            }

            return repository;  
        }

        public static InMemoryRepository<User> GetUsersRepository(int usersCount = 20)
        {
            var repository = new InMemoryRepository<User>();

            for (int i = 0; i < usersCount; i++)
            {
                var user = new User()
                {
                    UserName = "Username" + i,
                    FirstName = "User" + i,
                    LastName = "User lastName"  + i
                };

                repository.Add(user);
            }

            return repository;
        }
    }
}

namespace ChatSystem.Services.Data
{
    using System;
    using System.Linq;
    using ChatSystem.Data;
    using ChatSystem.Data.Repository;
    using ChatSystem.Common.Constants;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;

    public class MessagesService : IMessagesService
    {
        private readonly IRepository<ChatMessage> messages;
        private readonly IRepository<User> users;

        public MessagesService(IRepository<ChatMessage> messagesRepo,
            IRepository<User> usersRepo)
        {
            this.messages = messagesRepo;
            this.users = usersRepo;
        }

        public IQueryable<ChatMessage> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            return this.messages
                .All()
                .OrderByDescending(message => message.SentOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public void Add(string message, string sender, string receiver)
        {
            var currentUser = this.users
                .All()
                .FirstOrDefault(user => user.UserName == sender);

            var receiverUser = this.users
                .All()
                .FirstOrDefault(user => user.UserName == receiver);

            var newMessage = new ChatMessage()
            {
                Message = message,
                SentOn = DateTime.Now,
            };
            newMessage.Sender = currentUser;
            newMessage.Receiver = receiverUser;

            this.messages.Add(newMessage);
            this.messages.SaveChanges();
        }
    }
}

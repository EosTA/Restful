namespace ChatSystem.Services.Data
{
    using System;
    using System.Linq;
    using ChatSystem.Data.Repository;
    using ChatSystem.Common.Constants;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;
    using Common.Exceptions;

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

        public IQueryable<ChatMessage> All(string correspondent, string requestAskPerson, int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var requestTaker = this.users
                .All()
                .FirstOrDefault(user => user.UserName == correspondent);

            var requestAsker = this.users
                .All()
                .FirstOrDefault(user => user.UserName == requestAskPerson);

            if(requestTaker == null || requestAsker == null)
            {
                throw new NotCorrectCorrespondentProvidedException();
            }

            var result = this.messages
                .All()
                .Where(message => !message.IsDeleted)
                .Where(message =>
                (message.ReceiverId == requestTaker.Id && message.Sender.Id == requestAsker.Id) ||
                (message.ReceiverId == requestAsker.Id && message.Sender.Id == requestTaker.Id))
                .OrderByDescending(message => message.SentOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return result;
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

        public bool ChangeMessage(int messageId, bool isChangingDate, string newMessage, string asker)
        {
            var message = this.GetMessage(messageId).FirstOrDefault();

            if (message == null)
            {
                return false;
            }

            if (message.Sender.UserName != asker)
            {
                return false;
            }



            if (isChangingDate)
            {
                var date = DateTime.Now;
                message.ReadOn = date;
            }

            message.Message = newMessage;
            this.messages.SaveChanges();

            return true;
        }

        public IQueryable<ChatMessage> GetMessage(int id)
        {
            var result = this.messages
                .All()
                .Where(message => message.Id == id && !message.IsDeleted);

            return result;
        }

        public bool SetReadToAll(string asker, string correspondent)
        {
            var currentUser = this.users
               .All()
               .FirstOrDefault(user => user.UserName == asker);

            var receiverUser = this.users
                .All()
                .FirstOrDefault(user => user.UserName == correspondent);

            if (receiverUser == null || currentUser == null)
            {
                return false;
            }

            var result = this.messages
                .All()
                .Where(message =>
                (message.ReceiverId == currentUser.Id && message.Sender.Id == receiverUser.Id) ||
                (message.ReceiverId == receiverUser.Id && message.Sender.Id == currentUser.Id))
                .ToList();

            if (result.Count == 0)
            {
                return false;
            }

            var date = DateTime.Now;

            foreach (var singleMessage in result)
            {
                singleMessage.ReadOn = date;
            }
            this.messages.SaveChanges();

            return true;
        }

        public bool DeleteMessage(int id, string asker)
        {
            var message = this.GetMessage(id).FirstOrDefault();

            if (message == null)
            {
                return false;
            }

            if (message.Sender.UserName != asker)
            {
                return false;
            }

            message.IsDeleted = true;
            this.messages.SaveChanges();

            return true;
        }
    }
}

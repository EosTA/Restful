namespace ChatSystem.Services.Data
{
    using System;
    using System.Linq;
    using ChatSystem.Data.Repository;
    using ChatSystem.Common.Constants;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;
    using Common.Exceptions;

    public class UserService
    {
        private readonly IRepository<ChatMessage> messages;
        private readonly IRepository<User> users;

        public UserService(IRepository<ChatMessage> messagesRepo,
            IRepository<User> usersRepo)
        {
            this.messages = messagesRepo;
            this.users = usersRepo;
        }


        public IQueryable<User> All()
        {
            return this.users.All();
        }


    }
}

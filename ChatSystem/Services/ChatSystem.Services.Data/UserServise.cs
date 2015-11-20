namespace ChatSystem.Services.Data
{
    using System.Linq;

    using ChatSystem.Data.Models;
    using ChatSystem.Data.Repository;

    using Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<ChatMessage> messages;
        private readonly IRepository<User> users;

        public UserService(IRepository<ChatMessage> messagesRepo, IRepository<User> usersRepo)
        {
            this.messages = messagesRepo;
            this.users = usersRepo;
        }
        
        public IQueryable<User> All(string correspondent)
        {
            return this.users
                .All()
                .Where(x => x.UserName != correspondent);
        }
    }
}

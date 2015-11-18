namespace ChatSystem.Services.Data
{
    using System;
    using System.Linq;
    using ChatSystem.Data.Repository;
    using ChatSystem.Services.Data.Contracts;
    using Models;
    using Common.Constants;

    public class PresenceService : IPresenceService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Presence> presences;

        public PresenceService(IRepository<User> usersRepo, IRepository<Presence> presencesRepo)
        {
            this.users = usersRepo;
            this.presences = presencesRepo;
        }

        public void RegisterPresence(User user)
        {
            this.presences.Add(new Presence
            {
                RegisteredOn = DateTime.Now,
                User = user,
            });

            this.presences.SaveChanges();
        }

        public bool CheckPresence(string username)
        {
            var userFromDb = this.users
                .All()
                .FirstOrDefault(user => user.UserName == username);

            var result = this.presences
                .All()
                .Where(p => p.User.Id == userFromDb.Id)
                .FirstOrDefault();

            if(result != null)
            {
                TimeSpan difference = DateTime.Now - result.RegisteredOn;
                if(difference.TotalMinutes < GlobalConstants.PresenceMinutes)
                {
                    return true;
                }
            }
            return false;     
        }

        public void UpdatePresence(string username)
        {
            var userFromDb = this.users
                .All()
                .FirstOrDefault(user => user.UserName == username);

            var result = this.presences
                .All()
                .Where(p => p.User.Id == userFromDb.Id)
                .FirstOrDefault();

            if (result != null)
            {
                TimeSpan difference = DateTime.Now - result.RegisteredOn;
                if (difference.TotalMinutes > GlobalConstants.PresenceMinutes)
                {
                    result.RegisteredOn = DateTime.Now;
                }
            }
            else
            {
                RegisterPresence(userFromDb);
            }
        }
    }
}

namespace ChatSystem.Services.Data
{
    using System;
    using System.Linq;
    using ChatSystem.Api.Common;
    using ChatSystem.Common.Constants;
    using ChatSystem.Data.Repository;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;
    using Spring.IO;

    public class AvatarsService : IAvatarsService
    {
        private readonly IRepository<User> users;

        public AvatarsService(IRepository<User> usersRepo)
        {
            this.users = usersRepo;
        }

        public string Get(string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);
            var avatarUrl = user.AvatarUrl;

            return avatarUrl;
        }

        public void Post(IResource resource, string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);
            var dropboxClient = new DropBoxClient(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);
            string storeUrl = dropboxClient.Upload(resource, Guid.NewGuid() + ".jpg");
            user.AvatarUrl = storeUrl;
            this.users.SaveChanges();
        }

        public void Delete(string username)
        {
            this.users.All()
                .FirstOrDefault(u => u.UserName == username)
                .AvatarUrl = null;
            var dropboxClient = new DropBoxClient(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);
            //// TODO: delete image from dropbox;
        }
    }
}

namespace ChatSystem.Services.Data
{
    using ChatSystem.Api.Common;
    using ChatSystem.Common.Constants;
    using ChatSystem.Data.Repository;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using System;
    using System.IO;
    using System.Linq;

    public class AvatarsService : IAvatarsService
    {
        private readonly IRepository<User> users;

        public AvatarsService(IRepository<User> usersRepo)
        {
            this.users = usersRepo;
        }

        public DropboxFile Get(string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);

            var dropboxClient = new DropBoxController(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);
            var avatarUrl = user.AvatarUrl;

            var file = dropboxClient.GetFile(avatarUrl);

            return file;
        }

        public void Post(IResource resource, string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);

            var dropboxClient = new DropBoxController(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);

            string url = dropboxClient.Upload(resource, Guid.NewGuid() + ".bmp");

            user.AvatarUrl = "https://www.dropbox.com/home/Apps/ChatSystem?preview=" + url;
            this.users.SaveChanges();
        }

        public void Delete(string username)
        {
            this.users.All()
                .FirstOrDefault(u => u.UserName == username)
                .AvatarUrl = null;

            // TODO: delete image from dropbox;
        }
    }
}

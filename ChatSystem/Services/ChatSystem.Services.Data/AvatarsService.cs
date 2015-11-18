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

        public FileStream Get(string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);

            var dropboxClient = new DropBoxClient(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);
            var avatarUrl = user.AvatarUrl;

            var dropboxFile = dropboxClient.GetFile(avatarUrl);

            var extension = dropboxFile.Metadata.MimeType.Split('-')[2];
            var tempStorePath= username + '.' + extension;
            
            File.WriteAllBytes(tempStorePath, dropboxFile.Content);

            return File.Open(tempStorePath,FileMode.Open);
        }

        public void Post(IResource resource, string username)
        {
            var user = this.users.All().FirstOrDefault(u => u.UserName == username);

            var dropboxClient = new DropBoxClient(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);

            string storeUrl = dropboxClient.Upload(resource, Guid.NewGuid() + ".bmp");

            user.AvatarUrl = storeUrl;
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

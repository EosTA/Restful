namespace ChatSystem.Services.Data
{
    using ChatSystem.Api.Common;
    using ChatSystem.Common.Constants;
    using ChatSystem.Data.Repository;
    using ChatSystem.Models;
    using ChatSystem.Services.Data.Contracts;
    using System.Linq;

    class AvatarsService : IAvatarsService
    {
        private readonly IRepository<User> users;

        public AvatarsService(IRepository<User> usersRepo)
        {
            this.users = usersRepo;
        }

        public void Post(string avatarUrl, string username)
        {
            var dropboxClient = new DropBoxController(AuthorizationConstants.DropboxAppKey, AuthorizationConstants.DropboxAppSecret);
            this.users.All()
                .FirstOrDefault(u => u.UserName == username)
                .AvatarUrl = avatarUrl;
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

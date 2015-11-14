namespace ChatSystem.Services.Data.Contracts
{
    using Spring.IO;
using Spring.Social.Dropbox.Api;

    public interface IAvatarsService
    {
        void Post(IResource resource, string username);

        void Delete(string username);

        DropboxFile Get(string username);
    }
}

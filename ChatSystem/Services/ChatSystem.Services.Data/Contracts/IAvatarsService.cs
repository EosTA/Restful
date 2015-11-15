namespace ChatSystem.Services.Data.Contracts
{
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using System.IO;

    public interface IAvatarsService
    {
        void Post(IResource resource, string username);

        void Delete(string username);

        FileStream Get(string username);
    }
}

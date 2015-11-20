namespace ChatSystem.Services.Data.Contracts
{
    using Spring.IO;

    public interface IAvatarsService
    {
        void Post(IResource resource, string username);

        void Delete(string username);

        string Get(string username);
    }
}

namespace ChatSystem.Services.Data.Contracts
{
    using ChatSystem.Data.Models;

    public interface IPresenceService
    {
        void RegisterPresence(User user);

        bool CheckPresence(string username);

        void UpdatePresence(string username);
    }
}

namespace ChatSystem.Services.Data.Contracts
{
    using System.Linq;
    using ChatSystem.Common.Constants;
    using ChatSystem.Models;

    public interface IMessagesService
    {
        IQueryable<ChatMessage> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

        void Add(string message, string sender, string receiver);
    }
}

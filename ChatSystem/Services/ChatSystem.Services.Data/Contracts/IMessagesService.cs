namespace ChatSystem.Services.Data.Contracts
{
    using System.Linq;
    using ChatSystem.Common.Constants;
    using ChatSystem.Data.Models;

    public interface IMessagesService
    {
        IQueryable<ChatMessage> All(string correspondent, string asker, int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

        void Add(string message, string sender, string receiver);

        IQueryable<ChatMessage> GetMessage(int id);

        bool ChangeMessage(int messageId, bool isChangingDate, string newMessage, string asker);

        bool SetReadToAll(string asker, string correspondent);

        bool DeleteMessage(int id, string asker);
    }
}

namespace ChatSystem.Api.Models.Messages
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    public class MessageResponseModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime SentOn { get; set; }

        public int IsMyMessage { get; set; }

        public static Expression<Func<ChatMessage, MessageResponseModel>> FromModel(string senderUsername)
        {
            return message => new MessageResponseModel
            {
                Id = message.Id,
                Message = message.Message,
                SentOn = message.SentOn,
                IsMyMessage = message.Sender.UserName == senderUsername ? 1 : 0
            };
        }
    }
}
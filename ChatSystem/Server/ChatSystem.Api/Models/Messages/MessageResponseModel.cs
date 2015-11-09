namespace ChatSystem.Api.Models.Messages
{
    using System;
    using System.Linq.Expressions;
    using ChatSystem.Models;

    public class MessageResponseModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ReadOn { get; set; }

        public static Expression<Func<ChatMessage, MessageResponseModel>> FromModel
        {
            get
            {
                return message => new MessageResponseModel
                {
                    Message = message.Message
                };
            }
        }
    }
}
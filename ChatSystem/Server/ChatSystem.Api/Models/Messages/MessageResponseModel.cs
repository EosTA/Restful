using System;

namespace ChatSystem.Api.Models.Messages
{
    public class MessageResponseModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ReadOn { get; set; }
    }
}
namespace ChatSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ChatSystem.Common.Constants;

    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ReadOn { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        [MaxLength(ValidationConstants.MaxMessageLength)]
        public string Message { get; set; }
    }
}

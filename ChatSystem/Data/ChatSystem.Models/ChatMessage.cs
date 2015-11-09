namespace ChatSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChatMessage
    {
        public int Id { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ReadOn { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
    }
}

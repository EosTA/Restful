namespace ChatSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ChatSystem.Common.Constants;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ReadOn { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(ValidationConstants.MaxMessageLength)]
        public string Message { get; set; }
    }
}

namespace ChatSystem.Api.Models.Messages
{
    using System.ComponentModel.DataAnnotations;
    using ChatSystem.Common.Constants;

    public class SaveMessageRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxMessageLength)]
        public string Message { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Receiver { get; set; }
    }
}
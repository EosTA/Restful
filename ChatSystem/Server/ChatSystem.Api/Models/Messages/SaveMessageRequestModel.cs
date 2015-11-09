namespace ChatSystem.Api.Models.Messages
{
    using System.ComponentModel.DataAnnotations;
    using ChatSystem.Common.Constants;

    public class SaveMessageRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxMessageLength)]
        public string Message { get; set; }
    }
}
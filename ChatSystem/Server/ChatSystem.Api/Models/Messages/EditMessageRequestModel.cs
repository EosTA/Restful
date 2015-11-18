namespace ChatSystem.Api.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ChatSystem.Common.Constants;

    public class EditMessageRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxMessageLength)]
        public string Message { get; set; }

        [Required]
        public bool IsChangingDate { get; set; }

        public bool IsValid()
        {
            var isValid = !string.IsNullOrEmpty(this.Message);

            return isValid;
        }
    }
}
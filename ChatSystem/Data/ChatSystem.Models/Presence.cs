namespace ChatSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Presence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        public DateTime RegisteredOn { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}

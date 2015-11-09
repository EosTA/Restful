namespace ChatSystem.Data
{
    using System.Data.Entity;
    using ChatSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ChatSystemDbContext : IdentityDbContext<User>
    {
        public ChatSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; } 

        public static ChatSystemDbContext Create()
        {
            return new ChatSystemDbContext();
        }
    }
}

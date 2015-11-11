namespace ChatSystem.Data
{
    using System.Data.Entity;
    using ChatSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Contracts;

    public class ChatSystemDbContext : IdentityDbContext<User>, IChatSystemDbContext
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

namespace ChatSystem.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class ChatSystemDbContext : IdentityDbContext<User>, IChatSystemDbContext
    {
        public ChatSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; } 

        public virtual IDbSet<Presence> Presences { get; set; }

        public static ChatSystemDbContext Create()
        {
            return new ChatSystemDbContext();
        }
    }
}

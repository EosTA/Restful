namespace ChatSystem.Data
{
    using ChatSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ChatSystemDbContext : IdentityDbContext<User>
    {
        public ChatSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ChatSystemDbContext Create()
        {
            return new ChatSystemDbContext();
        }
    }
}

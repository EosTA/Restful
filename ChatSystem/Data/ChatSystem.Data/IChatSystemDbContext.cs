namespace ChatSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ChatSystem.Models;

    public interface IChatSystemDbContext
    {
        IDbSet<ChatMessage> ChatMessages { get; set; }

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();
    }
}

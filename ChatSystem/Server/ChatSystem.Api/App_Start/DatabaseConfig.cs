namespace ChatSystem.Api
{
    using System.Data.Entity;
    using ChatSystem.Data;
    using ChatSystem.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatSystemDbContext, Configuration>());
            ChatSystemDbContext.Create().Database.Initialize(true);
        }
    }
}
namespace ChatSystem.Services.Data.Contracts
{
    using System.Linq;

    using ChatSystem.Data.Models;

    public interface IUserService
    {
        IQueryable<User> All(string correspondent);
    }
}
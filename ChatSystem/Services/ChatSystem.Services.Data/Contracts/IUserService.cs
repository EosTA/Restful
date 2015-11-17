namespace hatSystem.Services.Data.Contracts
{
    using System.Linq;
    using ChatSystem.Models;

    public interface IUserService
    {
        IQueryable<User> All();
    }
}
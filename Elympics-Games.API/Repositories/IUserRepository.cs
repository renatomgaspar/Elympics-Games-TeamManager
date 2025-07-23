using Elympics_Games.API.Data.Entities;

namespace Elympics_Games.API.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmailAsync(string email);
    }
}

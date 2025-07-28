using Elympics_Games.API.Data.Entities;

namespace Elympics_Games.API.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<bool> ExistsByCountryAsync(string country);

        Task<Team?> GetByCountryAsync(string country);
    }
}

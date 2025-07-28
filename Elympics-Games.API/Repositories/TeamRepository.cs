using Elympics_Games.API.Data;
using Elympics_Games.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elympics_Games.API.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByCountryAsync(string country)
        {
            return await GetByCountryAsync(country) != null;
        }

        public async Task<Team?> GetByCountryAsync(string country)
        {
            return await _context.Set<Team>()
                .FirstOrDefaultAsync(t => t.Country == country);
        }
    }
}

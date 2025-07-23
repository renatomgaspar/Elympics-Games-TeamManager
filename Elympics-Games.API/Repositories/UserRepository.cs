using Elympics_Games.API.Data;
using Elympics_Games.API.Data.Entities;

namespace Elympics_Games.API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}

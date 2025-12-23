using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;
using TabletopTournaments.Infrastructure.DbContexts;

namespace TabletopTournaments.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly TabletopTournamentsDbContext _dbContext;
        public PlayerRepository(TabletopTournamentsDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
        }
    }
}
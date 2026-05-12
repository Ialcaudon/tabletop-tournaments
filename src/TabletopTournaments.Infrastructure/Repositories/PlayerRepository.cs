using Microsoft.EntityFrameworkCore;
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

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _dbContext.Players.FindAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _dbContext.Players.ToListAsync();
        }
    }
}
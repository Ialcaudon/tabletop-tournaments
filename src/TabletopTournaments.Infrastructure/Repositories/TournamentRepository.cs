using Microsoft.EntityFrameworkCore;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;
using TabletopTournaments.Infrastructure.DbContexts;

namespace TabletopTournaments.Infrastructure.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TabletopTournamentsDbContext _dbContext;
        public TournamentRepository(TabletopTournamentsDbContext context) 
        {
            _dbContext = context;
        }

        public async Task AddAsync(Tournament tournament)
        {
            await _dbContext.Tournaments.AddAsync(tournament);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            return await _dbContext.Tournaments
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _dbContext.Tournaments.ToListAsync();
        }

        public async Task UpdateAsync(Tournament tournament)
        {
            _dbContext.Tournaments.Update(tournament);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tournament tournament)
        {
            _dbContext.Tournaments.Remove(tournament);
            await _dbContext.SaveChangesAsync();
        }
    }

}

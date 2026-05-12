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
            return await _dbContext.Tournaments.FindAsync(id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _dbContext.Tournaments.ToListAsync();
        }
    }

}

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
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

}

using TabletopTournaments.Core.Entities;

namespace TabletopTournaments.Core.Interfaces
{
    public interface ITournamentRepository
    {
        Task AddAsync(Tournament tournament);
        Task<IEnumerable<Tournament>> GetAllAsync();
    }
}

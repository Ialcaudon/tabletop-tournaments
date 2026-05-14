using TabletopTournaments.Core.Entities;

namespace TabletopTournaments.Core.Interfaces
{
    public interface ITournamentRepository
    {
        Task AddAsync(Tournament tournament);
        Task<Tournament?> GetByIdAsync(int id);
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task UpdateAsync(Tournament tournament);
        Task DeleteAsync(Tournament tournament);
    }
}

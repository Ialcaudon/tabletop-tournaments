using System.Collections.Concurrent;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Infrastructure.Repositories
{
    public class InMemoryTournamentRepository : ITournamentRepository
    {
        private readonly ConcurrentDictionary<int, Tournament> _tournaments = new();

        public Task AddAsync(Tournament tournament)
        {
            // Simulate ID generation if not present (assuming simplistic for InMemory)
            // In a real DB, ID is generated upon insertion usually.
            // Since ID has a private setter, we might need reflection or changing the design for InMemory to work perfectly 
            // if we rely on DB auto-increment.
            // However, typically Domain Entities shouldn't be defined by DB behavior.
            // For now, we just add it. If ID is 0, we can't easily set it without private reflection or a method on the entity.
            
            // NOTE: For pure DDD, identity generation strategy involves either UUIDs in constructor or Domain Service.
            // Since we use int Id, likely it's DB generated.
            // For this InMemory repo, we will just store it.
            // If the user needs the ID back, this InMemory repo might need to use reflection to set it 
            // OR we should switch to Guid Ids which are easier for this.
            // Let's assume for now we just store it.
            
            if (tournament.Id == 0)
            {
                // Simple hack for InMemory ID generation to allow testing flow if needed
                var newId = _tournaments.Count + 1;
                // Use reflection to set private property for testing purposes
                typeof(Tournament).GetProperty("Id")?.SetValue(tournament, newId);
            }

            _tournaments.TryAdd(tournament.Id, tournament);
            return Task.CompletedTask;
        }

        public Task<Tournament?> GetByIdAsync(int id)
        {
            _tournaments.TryGetValue(id, out var tournament);
            return Task.FromResult(tournament);
        }

        public Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return Task.FromResult(_tournaments.Values.AsEnumerable());
        }

        public Task UpdateAsync(Tournament tournament)
        {
            _tournaments[tournament.Id] = tournament;
            return Task.CompletedTask;
        }
    }
}

using System.Collections.Concurrent;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Infrastructure.Repositories
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly ConcurrentDictionary<int, Player> _players = new();

        public Task AddAsync(Player player)
        {
            if (player.Id == 0)
            {
                var newId = _players.Count + 1;
                // Using reflection for InMemory ID generation simulation
                typeof(Player).GetProperty("Id")?.SetValue(player, newId);
            }

            _players.TryAdd(player.Id, player);
            return Task.CompletedTask;
        }
    }
}

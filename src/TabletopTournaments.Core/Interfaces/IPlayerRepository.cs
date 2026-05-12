using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;

namespace TabletopTournaments.Core.Interfaces
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        Task<Player?> GetByIdAsync(int id);
        Task<IEnumerable<Player>> GetAllAsync();
    }
}

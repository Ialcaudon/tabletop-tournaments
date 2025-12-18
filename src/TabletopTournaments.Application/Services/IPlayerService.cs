using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;

namespace TabletopTournaments.Application.Services;

public interface IPlayerService
{
    Task<int> RegisterPlayerAsync(string name);
}
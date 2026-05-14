using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<int> RegisterPlayerAsync(string name)
    {
        var player = new Player(name);
        await _playerRepository.AddAsync(player);
        return player.Id;
    }

    public async Task<Player?> GetPlayerByIdAsync(int id)
    {
        return await _playerRepository.GetByIdAsync(id);
    }
}
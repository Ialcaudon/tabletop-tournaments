using System.Collections.Generic;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IPlayerRepository _playerRepository;

    public TournamentService(ITournamentRepository tournamentRepository, IPlayerRepository playerRepository)
    {
        _tournamentRepository = tournamentRepository;
        _playerRepository = playerRepository;
    }

    public async Task<int> CreateTournamentAsync(string name, DateTime date, GameSystem gameSystem)
    {
        var tournament = new Tournament(name, date, gameSystem);
        await _tournamentRepository.AddAsync(tournament);
        return tournament.Id;
    }

    public async Task<Tournament?> GetTournamentByIdAsync(int id)
    {
        return await _tournamentRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        return await _tournamentRepository.GetAllAsync();
    }

    public async Task<bool> AddPlayerToTournamentAsync(int tournamentId, int playerId)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
        if (tournament == null)
            return false;

        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player == null)
            return false;

        tournament.AddPlayer(player);
        await _tournamentRepository.UpdateAsync(tournament);
        return true;
    }

    public async Task<bool> UpdateTournamentAsync(int id, string name, DateTime date, GameSystem gameSystem)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id);
        if (tournament == null)
            return false;

        tournament.Update(name, date, gameSystem);
        await _tournamentRepository.UpdateAsync(tournament);
        return true;
    }

    public async Task<bool> DeleteTournamentAsync(int id)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id);
        if (tournament == null)
            return false;

        await _tournamentRepository.DeleteAsync(tournament);
        return true;
    }
}
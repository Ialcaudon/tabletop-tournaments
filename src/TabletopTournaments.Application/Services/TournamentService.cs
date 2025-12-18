using System.Collections.Generic;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<int> CreateTournamentAsync(string name, DateTime date, GameSystem gameSystem)
    {
        var tournament = new Tournament(name, date, gameSystem);
        await _tournamentRepository.AddAsync(tournament);
        return tournament.Id;
    }

    public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        return await _tournamentRepository.GetAllAsync();
    }
}
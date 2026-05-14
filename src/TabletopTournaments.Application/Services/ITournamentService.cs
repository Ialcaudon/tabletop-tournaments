using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;

namespace TabletopTournaments.Application.Services;

public interface ITournamentService
{
    Task<int> CreateTournamentAsync(string name, DateTime date, GameSystem gameSystem);
    Task<Tournament?> GetTournamentByIdAsync(int id);
    Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
    Task<bool> AddPlayerToTournamentAsync(int tournamentId, int playerId);
    Task<bool> UpdateTournamentAsync(int id, string name, DateTime date, GameSystem gameSystem);
    Task<bool> DeleteTournamentAsync(int id);
}
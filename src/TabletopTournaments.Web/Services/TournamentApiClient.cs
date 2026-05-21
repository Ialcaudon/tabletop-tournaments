using System.Net.Http.Json;
using TabletopTournaments.Web.Models;

namespace TabletopTournaments.Web.Services;

public class TournamentApiClient
{
    private readonly HttpClient _httpClient;

    public TournamentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TournamentDto>> GetAllTournamentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TournamentDto>>("api/tournaments") ?? new();
    }

    public async Task<TournamentDto?> GetTournamentByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<TournamentDto>($"api/tournaments/{id}");
    }

    public async Task<bool> CreateTournamentAsync(CreateTournamentModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/tournaments", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTournamentAsync(int id, CreateTournamentModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/tournaments/{id}", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTournamentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/tournaments/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddPlayerToTournamentAsync(int tournamentId, int playerId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/tournaments/{tournamentId}/players", new { PlayerId = playerId });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemovePlayerFromTournamentAsync(int tournamentId, int playerId)
    {
        var response = await _httpClient.DeleteAsync($"api/tournaments/{tournamentId}/players/{playerId}");
        return response.IsSuccessStatusCode;
    }
}


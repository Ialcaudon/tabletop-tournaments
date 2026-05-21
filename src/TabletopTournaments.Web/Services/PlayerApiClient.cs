using System.Net.Http.Json;
using TabletopTournaments.Web.Models;

namespace TabletopTournaments.Web.Services;

public class PlayerApiClient
{
    private readonly HttpClient _httpClient;

    public PlayerApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlayerDto?> GetPlayerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PlayerDto>($"api/players/{id}");
    }

    public async Task<bool> RegisterPlayerAsync(RegisterPlayerModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/players", model);
        return response.IsSuccessStatusCode;
    }
}


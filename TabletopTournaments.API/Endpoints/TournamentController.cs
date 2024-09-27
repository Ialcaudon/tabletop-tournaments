using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Services;

namespace TabletopTournaments.API.Endpoints;

public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet("api/Tournaments")]
    public async Task<IActionResult> GetTournaments()
    {
        return Ok();
    }
}
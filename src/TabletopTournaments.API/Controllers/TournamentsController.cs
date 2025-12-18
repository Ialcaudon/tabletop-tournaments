using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Services;
using TabletopTournaments.Core.Enums;

namespace TabletopTournaments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTournamentRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest();
            }

            var id = await _tournamentService.CreateTournamentAsync(request.Name, request.Date, request.GameSystem);
            return CreatedAtAction(nameof(Create), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            return Ok(tournaments);
        }
    }

    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public GameSystem GameSystem { get; set; }
    }
}

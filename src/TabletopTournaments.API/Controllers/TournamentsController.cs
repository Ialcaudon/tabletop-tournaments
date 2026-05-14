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
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
                return NotFound();

            return Ok(tournament);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpPost("{id}/players")]
        public async Task<IActionResult> AddPlayer(int id, [FromBody] AddPlayerToTournamentRequest request)
        {
            if (request == null || request.PlayerId <= 0)
                return BadRequest();

            var result = await _tournamentService.AddPlayerToTournamentAsync(id, request.PlayerId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public GameSystem GameSystem { get; set; }
    }

    public class AddPlayerToTournamentRequest
    {
        public int PlayerId { get; set; }
    }
}

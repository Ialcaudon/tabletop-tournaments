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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTournamentRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest();

            var result = await _tournamentService.UpdateTournamentAsync(id, request.Name, request.Date, request.GameSystem);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tournamentService.DeleteTournamentAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}/players/{playerId}")]
        public async Task<IActionResult> RemovePlayer(int id, int playerId)
        {
            var result = await _tournamentService.RemovePlayerFromTournamentAsync(id, playerId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public GameSystem GameSystem { get; set; }
    }

    public class UpdateTournamentRequest
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public GameSystem GameSystem { get; set; }
    }

    public class AddPlayerToTournamentRequest
    {
        public int PlayerId { get; set; }
    }
}

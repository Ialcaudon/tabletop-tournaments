using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Tournaments.Commands.CreateTournament;

namespace TabletopTournaments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly CreateTournamentCommandHandler _createTournamentHandler;

        public TournamentsController(CreateTournamentCommandHandler createTournamentHandler)
        {
            _createTournamentHandler = createTournamentHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTournamentCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var id = await _createTournamentHandler.Handle(command, CancellationToken.None);
            return CreatedAtAction(nameof(Create), new { id }, id);
        }
    }
}

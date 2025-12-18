using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Tournaments.Commands.CreateTournament;
using TabletopTournaments.Application.Tournaments.Queries.GetAllTournaments;

namespace TabletopTournaments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly CreateTournamentCommandHandler _createTournamentHandler;
        private readonly GetAllTournamentsQueryHandler _getAllTournamentsHandler;

        public TournamentsController(CreateTournamentCommandHandler createTournamentHandler, GetAllTournamentsQueryHandler getAllTournamentsHandler)
        {
            _createTournamentHandler = createTournamentHandler;
            _getAllTournamentsHandler = getAllTournamentsHandler;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _getAllTournamentsHandler.Handle(new GetAllTournamentsQuery(), CancellationToken.None);
            return Ok(tournaments);
        }
    }
}

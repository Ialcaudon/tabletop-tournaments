using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Players.Commands.RegisterPlayer;

namespace TabletopTournaments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly RegisterPlayerCommandHandler _registerPlayerHandler;

        public PlayersController(RegisterPlayerCommandHandler registerPlayerHandler)
        {
            _registerPlayerHandler = registerPlayerHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPlayerCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var id = await _registerPlayerHandler.Handle(command, CancellationToken.None);
            return CreatedAtAction(nameof(Register), new { id }, id);
        }
    }
}

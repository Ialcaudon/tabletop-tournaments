using Microsoft.AspNetCore.Mvc;
using TabletopTournaments.Application.Services;

namespace TabletopTournaments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPlayerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest();
            }

            var id = await _playerService.RegisterPlayerAsync(request.Name);
            return CreatedAtAction(nameof(Register), new { id }, id);
        }
    }

    public class RegisterPlayerRequest
    {
        public string Name { get; set; }
    }
}

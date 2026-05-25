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
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players.Select(p => new { p.Id, p.Name }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }
    }

    public class RegisterPlayerRequest
    {
        public string Name { get; set; }
    }
}

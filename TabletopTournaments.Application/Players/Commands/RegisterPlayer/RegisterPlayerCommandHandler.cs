using System.Threading;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Players.Commands.RegisterPlayer
{
    public class RegisterPlayerCommandHandler
    {
        private readonly IPlayerRepository _playerRepository;

        public RegisterPlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<int> Handle(RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player(command.Name);
            await _playerRepository.AddAsync(player);
            return player.Id;
        }
    }
}

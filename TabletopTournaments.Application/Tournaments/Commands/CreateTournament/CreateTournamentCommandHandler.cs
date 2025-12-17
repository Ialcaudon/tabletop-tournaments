using System.Threading;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Tournaments.Commands.CreateTournament
{
    public class CreateTournamentCommandHandler
    {
        private readonly ITournamentRepository _tournamentRepository;

        public CreateTournamentCommandHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<int> Handle(CreateTournamentCommand command, CancellationToken cancellationToken)
        {
            var tournament = new Tournament(command.Name, command.Date, command.GameSystem);
            await _tournamentRepository.AddAsync(tournament);
            return tournament.Id;
        }
    }
}

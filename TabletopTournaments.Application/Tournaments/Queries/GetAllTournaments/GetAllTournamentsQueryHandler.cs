using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;

namespace TabletopTournaments.Application.Tournaments.Queries.GetAllTournaments
{
    public class GetAllTournamentsQueryHandler
    {
        private readonly ITournamentRepository _tournamentRepository;

        public GetAllTournamentsQueryHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<IEnumerable<Tournament>> Handle(GetAllTournamentsQuery query, CancellationToken cancellationToken)
        {
            return await _tournamentRepository.GetAllAsync();
        }
    }
}
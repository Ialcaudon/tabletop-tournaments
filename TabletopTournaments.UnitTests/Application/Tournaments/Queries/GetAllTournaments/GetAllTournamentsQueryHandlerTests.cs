using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TabletopTournaments.Application.Tournaments.Queries.GetAllTournaments;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Core.Interfaces;
using Xunit;

namespace TabletopTournaments.UnitTests.Application.Tournaments.Queries.GetAllTournaments
{
    public class GetAllTournamentsQueryHandlerTests
    {
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly GetAllTournamentsQueryHandler _handler;

        public GetAllTournamentsQueryHandlerTests()
        {
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _handler = new GetAllTournamentsQueryHandler(_tournamentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllTournamentsFromRepository()
        {
            // Arrange
            var tournaments = new List<Tournament>
            {
                new Tournament("Tournament 1", DateTime.Today, GameSystem.WarhammerAoS),
                new Tournament("Tournament 2", DateTime.Today.AddDays(1), GameSystem.MagicTheGathering)
            };
            _tournamentRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(tournaments);

            var query = new GetAllTournamentsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(tournaments);
            _tournamentRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
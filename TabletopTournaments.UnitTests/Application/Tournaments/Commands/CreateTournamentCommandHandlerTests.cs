using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TabletopTournaments.Application.Tournaments.Commands.CreateTournament;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Core.Interfaces;
using Xunit;

namespace TabletopTournaments.UnitTests.Application.Tournaments.Commands
{
    public class CreateTournamentCommandHandlerTests
    {
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly CreateTournamentCommandHandler _handler;

        public CreateTournamentCommandHandlerTests()
        {
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _handler = new CreateTournamentCommandHandler(_tournamentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAndPersistTournament_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateTournamentCommand(
                "Warhammer Fest",
                DateTime.Today.AddDays(30),
                GameSystem.WarhammerAoS
            );

            // Act
            int tournamentId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _tournamentRepositoryMock.Verify(x => x.AddAsync(It.Is<Tournament>(t =>
                t.Name == command.Name &&
                t.Date == command.Date &&
                t.GameSystem == command.GameSystem
            )), Times.Once);
        }
    }
}

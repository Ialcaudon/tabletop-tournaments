using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TabletopTournaments.Application.Services;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Core.Interfaces;
using Xunit;

namespace TabletopTournaments.UnitTests.Application.Services
{
    public class TournamentServiceTests
    {
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly TournamentService _service;

        public TournamentServiceTests()
        {
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _service = new TournamentService(_tournamentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTournamentAsync_ShouldCreateAndPersistTournament_WhenParametersAreValid()
        {
            // Arrange
            var name = "Warhammer Fest";
            var date = DateTime.Today.AddDays(30);
            var gameSystem = GameSystem.WarhammerAoS;

            // Act
            int tournamentId = await _service.CreateTournamentAsync(name, date, gameSystem);

            // Assert
            _tournamentRepositoryMock.Verify(x => x.AddAsync(It.Is<Tournament>(t =>
                t.Name == name &&
                t.Date == date &&
                t.GameSystem == gameSystem
            )), Times.Once);
        }

        [Fact]
        public async Task GetTournamentByIdAsync_ShouldReturnTournament_WhenTournamentExists()
        {
            var tournament = new Tournament("Test Tournament", DateTime.Today.AddDays(10), GameSystem.Generic);
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);

            var result = await _service.GetTournamentByIdAsync(1);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Test Tournament");
            _tournamentRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetTournamentByIdAsync_ShouldReturnNull_WhenTournamentDoesNotExist()
        {
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Tournament?)null);

            var result = await _service.GetTournamentByIdAsync(999);

            result.Should().BeNull();
        }
    }
}
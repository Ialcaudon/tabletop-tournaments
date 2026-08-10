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
        private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Today);

        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly TournamentService _service;

        public TournamentServiceTests()
        {
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _service = new TournamentService(_tournamentRepositoryMock.Object, _playerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTournamentAsync_ShouldCreateAndPersistTournament_WhenParametersAreValid()
        {
            var name = "Warhammer Fest";
            var date = Today.AddDays(30);
            var gameSystem = GameSystem.WarhammerAoS;

            int tournamentId = await _service.CreateTournamentAsync(name, date, gameSystem);

            _tournamentRepositoryMock.Verify(x => x.AddAsync(It.Is<Tournament>(t =>
                t.Name == name &&
                t.Date == date &&
                t.GameSystem == gameSystem
            )), Times.Once);
        }

        [Fact]
        public async Task GetTournamentByIdAsync_ShouldReturnTournament_WhenTournamentExists()
        {
            var tournament = new Tournament("Test Tournament", Today.AddDays(10), GameSystem.Generic);
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

        [Fact]
        public async Task AddPlayerToTournamentAsync_ShouldReturnTrue_WhenBothExist()
        {
            var tournament = new Tournament("Test", Today.AddDays(5), GameSystem.Generic);
            var player = new Player("John");
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);
            _playerRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(player);

            var result = await _service.AddPlayerToTournamentAsync(1, 2);

            result.Should().BeTrue();
            tournament.Players.Should().Contain(player);
            _tournamentRepositoryMock.Verify(x => x.UpdateAsync(tournament), Times.Once);
        }

        [Fact]
        public async Task AddPlayerToTournamentAsync_ShouldReturnFalse_WhenTournamentNotFound()
        {
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Tournament?)null);

            var result = await _service.AddPlayerToTournamentAsync(1, 2);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddPlayerToTournamentAsync_ShouldReturnFalse_WhenPlayerNotFound()
        {
            var tournament = new Tournament("Test", Today.AddDays(5), GameSystem.Generic);
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);
            _playerRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync((Player?)null);

            var result = await _service.AddPlayerToTournamentAsync(1, 2);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateTournamentAsync_ShouldReturnTrue_WhenTournamentExists()
        {
            var tournament = new Tournament("Old Name", Today.AddDays(5), GameSystem.Generic);
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);

            var newDate = Today.AddDays(15);
            var result = await _service.UpdateTournamentAsync(1, "New Name", newDate, GameSystem.WarhammerAoS);

            result.Should().BeTrue();
            tournament.Name.Should().Be("New Name");
            tournament.Date.Should().Be(newDate);
            tournament.GameSystem.Should().Be(GameSystem.WarhammerAoS);
            _tournamentRepositoryMock.Verify(x => x.UpdateAsync(tournament), Times.Once);
        }

        [Fact]
        public async Task UpdateTournamentAsync_ShouldReturnFalse_WhenTournamentDoesNotExist()
        {
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Tournament?)null);

            var result = await _service.UpdateTournamentAsync(999, "Name", Today, GameSystem.Generic);

            result.Should().BeFalse();
            _tournamentRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTournamentAsync_ShouldReturnTrue_WhenTournamentExists()
        {
            var tournament = new Tournament("To Delete", Today.AddDays(5), GameSystem.Generic);
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);

            var result = await _service.DeleteTournamentAsync(1);

            result.Should().BeTrue();
            _tournamentRepositoryMock.Verify(x => x.DeleteAsync(tournament), Times.Once);
        }

        [Fact]
        public async Task DeleteTournamentAsync_ShouldReturnFalse_WhenTournamentDoesNotExist()
        {
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Tournament?)null);

            var result = await _service.DeleteTournamentAsync(999);

            result.Should().BeFalse();
            _tournamentRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Fact]
        public async Task RemovePlayerFromTournamentAsync_ShouldReturnTrue_WhenBothExistAndPlayerIsRegistered()
        {
            var tournament = new Tournament("Test", Today.AddDays(5), GameSystem.Generic);
            var player = new Player("John");
            tournament.AddPlayer(player);
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);
            _playerRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(player);

            var result = await _service.RemovePlayerFromTournamentAsync(1, 2);

            result.Should().BeTrue();
            tournament.Players.Should().NotContain(player);
            _tournamentRepositoryMock.Verify(x => x.UpdateAsync(tournament), Times.Once);
        }

        [Fact]
        public async Task RemovePlayerFromTournamentAsync_ShouldReturnFalse_WhenTournamentNotFound()
        {
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Tournament?)null);

            var result = await _service.RemovePlayerFromTournamentAsync(1, 2);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task RemovePlayerFromTournamentAsync_ShouldReturnFalse_WhenPlayerNotInTournament()
        {
            var tournament = new Tournament("Test", Today.AddDays(5), GameSystem.Generic);
            var player = new Player("John");
            _tournamentRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(tournament);
            _playerRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(player);

            var result = await _service.RemovePlayerFromTournamentAsync(1, 2);

            result.Should().BeFalse();
        }
    }
}

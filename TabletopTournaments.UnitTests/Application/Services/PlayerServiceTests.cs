using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TabletopTournaments.Application.Services;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;
using Xunit;

namespace TabletopTournaments.UnitTests.Application.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _service = new PlayerService(_playerRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterPlayerAsync_ShouldCreateAndPersistPlayer_WhenParametersAreValid()
        {
            // Arrange
            var name = "John Doe";

            // Act
            int playerId = await _service.RegisterPlayerAsync(name);

            // Assert
            _playerRepositoryMock.Verify(x => x.AddAsync(It.Is<Player>(p =>
                p.Name == name
            )), Times.Once);
        }
    }
}
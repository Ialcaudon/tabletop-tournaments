using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TabletopTournaments.Application.Players.Commands.RegisterPlayer;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Interfaces;
using Xunit;

namespace TabletopTournaments.UnitTests.Application.Players.Commands
{
    public class RegisterPlayerCommandHandlerTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly RegisterPlayerCommandHandler _handler;

        public RegisterPlayerCommandHandlerTests()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _handler = new RegisterPlayerCommandHandler(_playerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRegisterAndPersistPlayer_WhenCommandIsValid()
        {
            // Arrange
            var command = new RegisterPlayerCommand("Ignacio");

            // Act
            int playerId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _playerRepositoryMock.Verify(x => x.AddAsync(It.Is<Player>(p =>
                p.Name == command.Name
            )), Times.Once);
        }
    }
}

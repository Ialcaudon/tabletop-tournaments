using FluentAssertions;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Infrastructure.Repositories;
using Xunit;

namespace TabletopTournaments.IntegrationTests.Repositories;

public class PlayerRepositoryTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public PlayerRepositoryTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAsync_ShouldAddPlayerToDatabase()
    {
        // Arrange
        var repository = new PlayerRepository(_fixture.DbContext);
        var player = new Player("Test Player");

        // Act
        await repository.AddAsync(player);

        // Assert
        var addedPlayer = await _fixture.DbContext.Players.FindAsync(player.Id);
        addedPlayer.Should().NotBeNull();
        addedPlayer.Name.Should().Be("Test Player");
    }
}
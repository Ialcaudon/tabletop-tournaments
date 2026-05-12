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
    public async Task AddAsync_ShouldPersistPlayerToDatabase()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new PlayerRepository(dbContext);
        var player = new Player("Persisted Player");

        // Act
        await repository.AddAsync(player);

        // Assert
        player.Id.Should().BeGreaterThan(0);

        await using var verifyContext = _fixture.CreateDbContext();
        var persisted = await verifyContext.Players.FindAsync(player.Id);
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Persisted Player");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPlayer_WhenPlayerExists()
    {
        // Arrange
        await using var seedContext = _fixture.CreateDbContext();
        var player = new Player("Find Me");
        seedContext.Players.Add(player);
        await seedContext.SaveChangesAsync();

        // Act
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new PlayerRepository(dbContext);
        var result = await repository.GetByIdAsync(player.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Find Me");
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPlayers()
    {
        // Arrange
        await using var seedContext = _fixture.CreateDbContext();
        seedContext.Players.Add(new Player("Player A"));
        seedContext.Players.Add(new Player("Player B"));
        await seedContext.SaveChangesAsync();

        // Act
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new PlayerRepository(dbContext);
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().Contain(p => p.Name == "Player A");
        result.Should().Contain(p => p.Name == "Player B");
    }
}
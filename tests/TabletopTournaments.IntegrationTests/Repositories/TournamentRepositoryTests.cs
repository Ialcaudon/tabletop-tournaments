using FluentAssertions;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Infrastructure.Repositories;
using Xunit;

namespace TabletopTournaments.IntegrationTests.Repositories;

public class TournamentRepositoryTests : IClassFixture<IntegrationTestFixture>
{
    private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Today);

    private readonly IntegrationTestFixture _fixture;

    public TournamentRepositoryTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAsync_ShouldPersistTournamentToDatabase()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new TournamentRepository(dbContext);
        var tournament = new Tournament("Persisted Tournament", Today.AddDays(30), GameSystem.WarhammerAoS);

        // Act
        await repository.AddAsync(tournament);

        // Assert
        tournament.Id.Should().BeGreaterThan(0);

        await using var verifyContext = _fixture.CreateDbContext();
        var persisted = await verifyContext.Tournaments.FindAsync(tournament.Id);
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Persisted Tournament");
        persisted.GameSystem.Should().Be(GameSystem.WarhammerAoS);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTournament_WhenTournamentExists()
    {
        // Arrange
        var date = Today.AddDays(15);
        await using var seedContext = _fixture.CreateDbContext();
        var tournament = new Tournament("Find This Tournament", date, GameSystem.MagicTheGathering);
        seedContext.Tournaments.Add(tournament);
        await seedContext.SaveChangesAsync();

        // Act
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new TournamentRepository(dbContext);
        var result = await repository.GetByIdAsync(tournament.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Find This Tournament");
        result.Date.Should().Be(date);
        result.GameSystem.Should().Be(GameSystem.MagicTheGathering);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTournaments()
    {
        // Arrange
        await using var seedContext = _fixture.CreateDbContext();
        seedContext.Tournaments.Add(new Tournament("Tournament A", Today.AddDays(5), GameSystem.Generic));
        seedContext.Tournaments.Add(new Tournament("Tournament B", Today.AddDays(10), GameSystem.Catan));
        await seedContext.SaveChangesAsync();

        // Act
        await using var dbContext = _fixture.CreateDbContext();
        var repository = new TournamentRepository(dbContext);
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().Contain(t => t.Name == "Tournament A");
        result.Should().Contain(t => t.Name == "Tournament B");
    }

    [Fact]
    public async Task AddAsync_ShouldPersistTournamentPlayerRelationship()
    {
        await using var dbContext = _fixture.CreateDbContext();
        var player = new Player("Registered Player");
        var tournament = new Tournament("Tournament With Player", Today.AddDays(20), GameSystem.Generic);
        tournament.AddPlayer(player);
        var repository = new TournamentRepository(dbContext);

        await repository.AddAsync(tournament);

        await using var verifyContext = _fixture.CreateDbContext();
        var verifyRepository = new TournamentRepository(verifyContext);
        var persisted = await verifyRepository.GetByIdAsync(tournament.Id);

        persisted.Should().NotBeNull();
        persisted!.Players.Should().ContainSingle(p => p.Name == "Registered Player");
    }
}

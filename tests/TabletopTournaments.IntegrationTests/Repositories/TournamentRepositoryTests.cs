using FluentAssertions;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using TabletopTournaments.Infrastructure.Repositories;
using Xunit;

namespace TabletopTournaments.IntegrationTests.Repositories;

public class TournamentRepositoryTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public TournamentRepositoryTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAsync_ShouldAddTournamentToDatabase()
    {
        // Arrange
        var repository = new TournamentRepository(_fixture.DbContext);
        var tournament = new Tournament("Test Tournament", DateTime.Now, GameSystem.Generic);

        // Act
        await repository.AddAsync(tournament);

        // Assert
        var tournaments = await repository.GetAllAsync();
        tournaments.Should().Contain(t => t.Name == "Test Tournament");
    }
}
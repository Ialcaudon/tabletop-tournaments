using System;
using Microsoft.EntityFrameworkCore;
using TabletopTournaments.Infrastructure.DbContexts;
using Xunit;

namespace TabletopTournaments.IntegrationTests;

public class IntegrationTestFixture : IDisposable
{
    public TabletopTournamentsDbContext DbContext { get; }

    public IntegrationTestFixture()
    {
        var password = Environment.GetEnvironmentVariable("SA_PASSWORD") 
            ?? throw new InvalidOperationException("SA_PASSWORD environment variable is not set.");
        var connectionString = $"Server=localhost,1433;Database=TabletopTournamentsTest;User Id=sa;Password={password};TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<TabletopTournamentsDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        DbContext = new TabletopTournamentsDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}
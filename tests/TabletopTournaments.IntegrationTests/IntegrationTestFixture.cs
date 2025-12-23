using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using TabletopTournaments.Infrastructure.DbContexts;
using Xunit;

namespace TabletopTournaments.IntegrationTests;

public class IntegrationTestFixture : IDisposable
{
    public TabletopTournamentsDbContext DbContext { get; }

    public IntegrationTestFixture()
    {
        var password = GetSaPassword();
        var connectionString = $"Server=localhost,1433;Database=TabletopTournamentsTest;User Id=sa;Password={password};TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<TabletopTournamentsDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        DbContext = new TabletopTournamentsDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    private static string GetSaPassword()
    {
        var password = Environment.GetEnvironmentVariable("SA_PASSWORD");

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException("SA_PASSWORD environment variable not set.");
        }

        return password;
    }


    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}
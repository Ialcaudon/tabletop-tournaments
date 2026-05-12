using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using TabletopTournaments.Infrastructure.DbContexts;
using Xunit;

namespace TabletopTournaments.IntegrationTests;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer;
    public TabletopTournamentsDbContext DbContext { get; private set; } = null!;

    public IntegrationTestFixture()
    {
        var password = GetSaPassword();
        _sqlContainer = new MsSqlBuilder()
            .WithPassword(password)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

        var sqlConnectionBuilder = new SqlConnectionStringBuilder(_sqlContainer.GetConnectionString())
        {
            InitialCatalog = "TabletopTournamentsTest",
            TrustServerCertificate = true
        };

        var options = new DbContextOptionsBuilder<TabletopTournamentsDbContext>()
            .UseSqlServer(sqlConnectionBuilder.ConnectionString)
            .Options;

        DbContext = new TabletopTournamentsDbContext(options);
        await DbContext.Database.EnsureCreatedAsync();
    }

    private static string GetSaPassword()
    {
        var password = Environment.GetEnvironmentVariable("SA_PASSWORD")
            ?? Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");

        if (string.IsNullOrWhiteSpace(password))
        {
            password = TryReadSaPasswordFromDotEnv();
        }

        return string.IsNullOrWhiteSpace(password) ? "YourStrong!Passw0rd" : password;
    }

    private static string? TryReadSaPasswordFromDotEnv()
    {
        var directory = AppContext.BaseDirectory;

        while (!string.IsNullOrWhiteSpace(directory))
        {
            var envPath = Path.Combine(directory, ".env");
            if (File.Exists(envPath))
            {
                foreach (var rawLine in File.ReadAllLines(envPath))
                {
                    var line = rawLine.Trim();
                    if (line.StartsWith("#") || !line.StartsWith("SA_PASSWORD=", StringComparison.Ordinal))
                    {
                        continue;
                    }

                    return line["SA_PASSWORD=".Length..].Trim();
                }

                return null;
            }

            directory = Directory.GetParent(directory)?.FullName;
        }

        return null;
    }

    public async Task DisposeAsync()
    {
        if (DbContext is not null)
        {
            await DbContext.Database.EnsureDeletedAsync();
            await DbContext.DisposeAsync();
        }

        await _sqlContainer.DisposeAsync();
    }
}
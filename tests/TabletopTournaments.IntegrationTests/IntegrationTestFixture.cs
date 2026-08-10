using Microsoft.EntityFrameworkCore;
using Npgsql;
using TabletopTournaments.Infrastructure.DbContexts;
using Testcontainers.PostgreSql;
using Xunit;

namespace TabletopTournaments.IntegrationTests;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15")
        .WithDatabase("tabletop_tournaments_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private string _connectionString = null!;

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        _connectionString = _postgresContainer.GetConnectionString();
        await ApplyMigrationsAsync();
    }

    public TabletopTournamentsDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TabletopTournamentsDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        return new TabletopTournamentsDbContext(options);
    }

    public async Task<NpgsqlConnection> OpenConnectionAsync()
    {
        const int maximumAttempts = 20;

        for (var attempt = 1; attempt <= maximumAttempts; attempt++)
        {
            var connection = new NpgsqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                return connection;
            }
            catch (NpgsqlException) when (attempt < maximumAttempts)
            {
                await connection.DisposeAsync();
                await Task.Delay(TimeSpan.FromMilliseconds(250));
            }
        }

        throw new InvalidOperationException("PostgreSQL did not accept connections after starting.");
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
    }

    private async Task ApplyMigrationsAsync()
    {
        var migrationsDirectory = FindMigrationsDirectory();
        var migrationPaths = Directory.GetFiles(migrationsDirectory, "*.sql")
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();

        if (migrationPaths.Length == 0)
        {
            throw new InvalidOperationException($"No SQL migrations found in {migrationsDirectory}.");
        }

        await using var connection = await OpenConnectionAsync();
        foreach (var migrationPath in migrationPaths)
        {
            var sql = await File.ReadAllTextAsync(migrationPath);
            await using var command = new NpgsqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }
    }

    private static string FindMigrationsDirectory()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "supabase", "migrations");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException(
            "The supabase/migrations directory could not be found from the test output path.");
    }
}

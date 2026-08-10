using FluentAssertions;
using Npgsql;
using Xunit;

namespace TabletopTournaments.IntegrationTests;

public class DatabaseMigrationTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public DatabaseMigrationTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Migrations_ShouldCreateApplicationTablesOnlyInPrivateSchema()
    {
        await using var connection = await _fixture.OpenConnectionAsync();

        const string sql = """
            select table_name
            from information_schema.tables
            where table_schema = 'tabletop'
              and table_type = 'BASE TABLE'
            order by table_name;
            """;

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();
        var tableNames = new List<string>();

        while (await reader.ReadAsync())
        {
            tableNames.Add(reader.GetString(0));
        }

        tableNames.Should().Equal("players", "tournament_players", "tournaments");
    }

    [Fact]
    public async Task Migrations_ShouldMapTournamentDateAsCalendarDate()
    {
        await using var connection = await _fixture.OpenConnectionAsync();

        const string sql = """
            select data_type
            from information_schema.columns
            where table_schema = 'tabletop'
              and table_name = 'tournaments'
              and column_name = 'date';
            """;

        await using var command = new NpgsqlCommand(sql, connection);
        var dataType = (string?)await command.ExecuteScalarAsync();

        dataType.Should().Be("date");
    }

    [Fact]
    public async Task Migrations_ShouldNotExposeApplicationTablesInPublicSchema()
    {
        await using var connection = await _fixture.OpenConnectionAsync();

        const string sql = """
            select count(*)
            from information_schema.tables
            where table_schema = 'public'
              and table_name in ('players', 'tournament_players', 'tournaments');
            """;

        await using var command = new NpgsqlCommand(sql, connection);
        var tableCount = (long)(await command.ExecuteScalarAsync())!;

        tableCount.Should().Be(0);
    }

    [Fact]
    public async Task Migrations_ShouldIndexEveryForeignKey()
    {
        await using var connection = await _fixture.OpenConnectionAsync();

        const string sql = """
            select count(*)
            from pg_constraint constraint_definition
            join pg_attribute attribute
              on attribute.attrelid = constraint_definition.conrelid
             and attribute.attnum = any(constraint_definition.conkey)
            where constraint_definition.contype = 'f'
              and constraint_definition.connamespace = 'tabletop'::regnamespace
              and not exists (
                  select 1
                  from pg_index index_definition
                  where index_definition.indrelid = constraint_definition.conrelid
                    and attribute.attnum = any(index_definition.indkey)
              );
            """;

        await using var command = new NpgsqlCommand(sql, connection);
        var missingIndexCount = (long)(await command.ExecuteScalarAsync())!;

        missingIndexCount.Should().Be(0);
    }
}

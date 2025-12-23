# Tabletop Tournaments Architecture

## Overview
This project follows a simplified Domain-Driven Design (DDD) architecture for a .NET 8 Web API managing tabletop tournaments. The system allows users to create and manage tournaments, players, and related entities.

## Layers
- **Core/Domain**: Contains domain entities, value objects, interfaces, and enums. Entities enforce business rules with private setters and constructor validation.
- **Application**: Houses application services that orchestrate domain logic and handle use cases. Services inject repositories to perform operations.
- **Infrastructure**: Implements repository interfaces using EF Core (currently transitioning from in-memory to SQL Server). Includes DbContext and entity configurations.
- **API**: ASP.NET Core controllers that inject services and handle HTTP requests/responses.
- **UnitTests**: xUnit tests with Moq and FluentAssertions for unit testing services and domain logic.
- **IntegrationTests**: Planned for end-to-end testing of infrastructure and API interactions.

## Key Patterns
- **Entities**: Use private setters and public constructors for validation (e.g., `Tournament(string name, DateTime date, GameSystem gameSystem)`).
- **Application Services**: Inject repositories; handle business logic (e.g., `TournamentService.CreateTournament(...)`).
- **Repositories**: Interfaces in Core, implementations in Infrastructure. Currently in-memory with reflection for ID assignment; transitioning to EF Core with SQL Server.
- **Controllers**: Inject services; return IActionResult (CreatedAtAction for POST, Ok for GET).
- **Tests**: Unit tests mock repositories; integration tests will verify database interactions.

## Workflows
- Build: `dotnet build`
- Run: `dotnet run --project TabletopTournaments.API/TabletopTournaments.API.csproj --launch-profile https`
- Test: `dotnet test` (unit and integration)
- Debug: Use VS Code launch config ".NET Core Launch (web)"

## Domain Models Review
- **Tournament**: Correctly implements private setters, protected EF constructor, and validation in public constructor.
- **Player**: Has private setters and validation, but lacks a protected EF constructor. Recommend adding `protected Player() { }` to align with EF Core requirements.
- **GameSystem**: Simple enum for tournament types.

Ensure all entities follow this pattern for EF Core compatibility.

## Integration Tests Proposal
To ensure the infrastructure layer correctly persists data to the database, we propose adding integration tests. These tests will validate end-to-end functionality, including database insertions.

### Structure
- Create a new project: `TabletopTournaments.IntegrationTests` in the `tests/` folder.
- Use xUnit, FluentAssertions, and Microsoft.AspNetCore.Mvc.Testing for API tests.
- Configure a test database (e.g., SQL Server via Docker Compose or local instance) to avoid affecting production data.

### Patterns
- **Repository Tests**: Directly test repository implementations by injecting DbContext and verifying database state after operations.
- **API Tests**: Use TestServer to simulate HTTP requests and assert responses, including checking database changes.
- **Setup/Teardown**: Use fixtures to initialize/cleanup test data. For example, reset database state between tests.
- **Database Verification**: After operations, query the database to confirm insertions/updates (e.g., using EF Core or raw SQL).

### Example Workflow
1. Start test database (e.g., via Docker Compose).
2. Run tests: `dotnet test TabletopTournaments.IntegrationTests/TabletopTournaments.IntegrationTests.csproj`
3. Tests should cover CRUD operations on entities like Tournament and Player.

### Dependencies
- Add packages: xunit, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing, Microsoft.EntityFrameworkCore.SqlServer (for tests).

This ensures the transition to SQL Server (as per story 001-002) is validated through automated tests.
# AI Coding Guidelines for Tabletop Tournaments

## Architecture Overview
This is a Clean Architecture .NET 8 Web API with CQRS commands. Layers:
- **Core**: Domain entities, interfaces, enums. Entities use private setters and constructor validation.
- **Application**: Command handlers and services. Commands are immutable with constructor parameters.
- **Infrastructure**: Repository implementations (currently in-memory, EF Core ready).
- **API**: ASP.NET Core controllers injecting handlers directly (no mediator).
- **UnitTests**: xUnit tests with Moq and FluentAssertions.

## Key Patterns
- Commands: Immutable classes with public getters, e.g., `CreateTournamentCommand(string name, DateTime date, GameSystem gameSystem)`.
- Handlers: Inject repositories, create entities, call repo methods, e.g., `CreateTournamentCommandHandler`.
- Entities: Private setters, protected EF constructor, validation in public constructor.
- Repositories: Interfaces in Core, implementations in Infrastructure.
- Controllers: Minimal, inject handlers, return IActionResult with CreatedAtAction for POSTs.

## Workflows
- **Build**: `dotnet build` or VS Code task "build".
- **Run**: `dotnet run --project TabletopTournaments.API/TabletopTournaments.API.csproj --launch-profile https` (Swagger at https://localhost:7187/swagger).
- **Watch**: `dotnet watch run --project TabletopTournaments.API/TabletopTournaments.API.csproj`.
- **Test**: `dotnet test` in UnitTests project.
- **Debug**: Use VS Code launch config ".NET Core Launch (web)" with preLaunchTask "build".

## Conventions
- Nullable enabled, implicit usings.
- GameSystem enum for tournament types.
- In-memory repos use reflection for ID assignment (temporary).
- EF DbContext with entity configurations in Infrastructure.
- Tests mock repositories, verify calls with FluentAssertions.

## Examples
- New command: Add to Application/Tournaments/Commands/, handler class, register in Startup.cs.
- Entity: Add to Core/Entities/, with private setters and validation.
- Test: Mock repo, assert handler calls repo.AddAsync with correct entity.</content>
<parameter name="filePath">/Users/ignacio/Repos/tabletop-tournaments/.github/copilot-instructions.md
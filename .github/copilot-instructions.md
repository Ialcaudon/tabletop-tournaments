# AI Coding Guidelines for Tabletop Tournaments

## Architecture Overview
This is a simplified Domain-Driven Design (DDD) .NET 8 Web API. Layers:
- **Core/Domain**: Domain entities, value objects, interfaces, enums. Entities use private setters and constructor validation.
- **Application**: Application services that orchestrate domain logic and handle use cases.
- **Infrastructure**: Repository implementations (currently in-memory, EF Core ready with DbContext and entity configs).
- **API**: ASP.NET Core controllers injecting services directly.
- **UnitTests**: xUnit tests with Moq and FluentAssertions.
- **IntegrationTests**: Planned for future integration tests.

## Key Patterns
- Entities: Private setters, protected EF constructor, validation in public constructor, e.g., `Tournament(string name, DateTime date, GameSystem gameSystem)`.
- Application Services: Inject repositories, handle business logic, e.g., `TournamentService.CreateTournament(name, date, gameSystem)`.
- Repositories: Interfaces in Core, implementations in Infrastructure; in-memory uses reflection for ID assignment.
- Controllers: Inject services, call service methods, return IActionResult with CreatedAtAction for POSTs, Ok for GETs.

## Workflows
- **Build**: `dotnet build` or VS Code task "build".
- **Run**: `dotnet run --project TabletopTournaments.API/TabletopTournaments.API.csproj --launch-profile https` (Swagger at https://localhost:7187/swagger).
- **Watch**: `dotnet watch run --project TabletopTournaments.API/TabletopTournaments.API.csproj`.
- **Test**: `dotnet test` in UnitTests project.
- **Debug**: Use VS Code launch config ".NET Core Launch (web)" with preLaunchTask "build".

## Conventions
- Nullable enabled, implicit usings.
- GameSystem enum for tournament types (Generic, WarhammerAoS, etc.).
- In-memory repos use reflection for ID assignment (temporary hack).
- EF DbContext with entity configurations in Infrastructure.
- Tests mock repositories, verify service calls with FluentAssertions.
- Name methods/classes descriptively, follow C# conventions.
- No comments unless necessary for clarity.
- Register services as transient, repos as singleton in Startup.cs.

## Examples
- New feature: Add service method in Application/Services/, inject in controller, register in Startup.cs.
- Entity: Add to Core/Entities/, with private setters and validation.
- Test: Mock repo, assert service calls repo methods with correct parameters.</content>
<parameter name="filePath">/Users/ignacio/Repos/tabletop-tournaments/.github/copilot-instructions.md
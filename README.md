# Tabletop Tournaments

A Clean Architecture based .NET 8 Web API for managing tabletop tournaments.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) (Recommended)

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/Ialcaudon/tabletop-tournaments.git
cd tabletop-tournaments
```

### 2. Build the project
```bash
dotnet build
```

### 3. Run the project

#### Using Terminal (Universal)
This works in any editor or terminal.
```bash
dotnet run --project TabletopTournaments.API/TabletopTournaments.API.csproj --launch-profile https
```
The application will start and listen on the configured ports (usually **https://localhost:7187**).

#### Using VS Code (Optional)
If your editor supports VS Code compatible launch configurations (like the `.vscode` folder):
1. Open the "Run and Debug" panel.
2. Select **.NET Core Launch (web)**.
3. Start debugging.

## API Documentation (Swagger)

When running in **Development** mode (which is default for VS Code launch), Swagger UI is available at:

```
https://localhost:7187/swagger
```
*(Note: Port may vary, check your `launchSettings.json` or terminal output)*

## Project Structure

- **Core**: Domain entities, interfaces, and business logic.
- **Application**: Application use cases (CQRS Commands/Queries).
- **Infrastructure**: Implementation of interfaces (Initial Repositories, etc.).
- **API**: ASP.NET Core Web API entry point.
- **UnitTests**: Unit tests for Core and Application layers.
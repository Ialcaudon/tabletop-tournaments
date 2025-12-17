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

#### Using VS Code (Recommended)
1. Open the folder in VS Code.
2. Press **F5** (or go to **Run and Debug** > **.NET Core Launch (web)**).
3. The browser will automatically open to valid endpoints, or you can navigate to the Swagger documentation manually.

#### Using Terminal
```bash
dotnet run --project TabletopTournaments.API/TabletopTournaments.API.csproj
```

## API Documentation (Swagger)

When running in **Development** mode (which is default for VS Code launch), Swagger UI is available at:

```
https://localhost:7084/swagger
```
*(Note: Port may vary, check your `launchSettings.json` or terminal output)*

## Project Structure

- **Core**: Domain entities, interfaces, and business logic.
- **Application**: Application use cases (CQRS Commands/Queries).
- **Infrastructure**: Implementation of interfaces (Initial Repositories, etc.).
- **API**: ASP.NET Core Web API entry point.
- **UnitTests**: Unit tests for Core and Application layers.
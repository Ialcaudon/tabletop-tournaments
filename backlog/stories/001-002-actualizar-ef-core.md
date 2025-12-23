# Story 001-002: Actualizar configuración de EF Core para conectar a SQL Server

**Descripción:** Actualizar la configuración de EF Core en Infrastructure para conectar a SQL Server en lugar de in-memory.  

**Prioridad:** Alta  

**Estado:** READY  

**Tasks:**  
- Modificar TabletopTournamentsDbContext para usar SQL Server provider.
- Actualizar connection string en appsettings.json.
- Crear PlayerRepository.cs implementando IPlayerRepository con EF Core.
- Registrar DbContext en Startup.cs/Program.cs con SQL Server provider.
- Cambiar inyecciones de repositorios en Startup.cs de InMemory a EF (TournamentRepository y PlayerRepository).
- Añadir y configurar connection string en appsettings.json (usar localhost,1433 con credenciales del .env).
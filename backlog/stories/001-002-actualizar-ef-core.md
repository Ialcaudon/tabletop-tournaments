# Story 001-002: Actualizar configuración de EF Core para conectar a SQL Server

**Descripción:** Actualizar la configuración de EF Core en Infrastructure para conectar a SQL Server en lugar de in-memory.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Modificar TabletopTournamentsDbContext para usar SQL Server provider.
- [x] Actualizar connection string en appsettings.json.
- [x] Crear PlayerRepository.cs implementando IPlayerRepository con EF Core.
- [x] Registrar DbContext en Startup.cs/Program.cs con SQL Server provider.
- [x] Cambiar inyecciones de repositorios en Startup.cs de InMemory a EF (TournamentRepository y PlayerRepository).
- [x] Añadir y configurar connection string en appsettings.json (usar localhost,1433 con credenciales del .env).
- [x] Añadir constructor protegido EF Core a Player.
- [x] Unificar patrón SaveChangesAsync dentro de AddAsync en repos.
- [x] Actualizar paquetes NuGet a versiones 9.x (alineados con .NET 9).

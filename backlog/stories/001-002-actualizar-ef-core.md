# Story 001-002: Actualizar configuración de EF Core para conectar a SQL Server

**Descripción:** Actualizar la configuración de EF Core en Infrastructure para conectar a SQL Server en lugar de in-memory.  

**Prioridad:** Alta  

**Estado:** DRAFT  

**Tasks:**  
- Modificar TabletopTournamentsDbContext para usar SQL Server provider.  
- Actualizar connection string en appsettings.json.
# Story 001-003: Crear migraciones iniciales para entidades Player y Tournament

**Descripción:** Crear migraciones iniciales para las entidades Player y Tournament usando EF Core.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Instalar paquete `Microsoft.EntityFrameworkCore.Design` en el proyecto API (necesario para el tooling de EF).  
- [x] Verificar/instalar la herramienta global `dotnet-ef`.  
- [x] Ejecutar `dotnet ef migrations add InitialCreate` (target: Infrastructure, startup: API).  
- [x] Levantar el contenedor Docker de SQL Server.  
- [x] Corregir connection string en appsettings.json (alinear contraseña con .env).  
- [x] Aplicar la migración con `dotnet ef database update`.  
- [x] Verificar que las tablas `Players` y `Tournaments` existen en la BD.  

**Requisitos previos:**  
- Story 001-001 (Docker SQL Server) completada.  
- Story 001-002 (EF Core configurado con SQL Server) completada.

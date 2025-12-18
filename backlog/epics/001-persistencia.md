# Epic 001: Persistencia

**Descripción:** Configurar una base de datos SQL (e.g., SQL Server) y un contenedor Docker para persistir datos de usuarios/jugadores y torneos, reemplazando el almacenamiento en memoria actual.  

**Prioridad:** Alta (base fundamental para el resto).  

**Estado:** DRAFT  

**Stories/Tasks:**  
- [001-001] Configurar SQL Server en contenedor Docker  
- [001-002] Actualizar configuración de EF Core para conectar a SQL Server  
- [001-003] Crear migraciones iniciales para entidades Player y Tournament  
- [001-004] Probar persistencia con tests unitarios
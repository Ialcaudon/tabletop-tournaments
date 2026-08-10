# Epic 001: Persistencia

**Descripción:** Mantener la persistencia de jugadores y torneos mediante Entity
Framework Core y migrarla de SQL Server a PostgreSQL administrado por Supabase en
producción.

**Prioridad:** Alta (base fundamental para el resto).  

**Estado:** IN_PROGRESS

**Historias/tareas:**
- [001-001] Configurar SQL Server en contenedor Docker  
- [001-002] Actualizar configuración de EF Core para conectar a SQL Server  
- [001-003] Crear migraciones iniciales para entidades Player y Tournament  
- [001-004] Probar persistencia con integration tests
- [001-005] Migrar la persistencia a PostgreSQL en Supabase

La configuración SQL Server de las historias 001-001 a 001-004 refleja la
implementación anterior. La transición vigente está definida en
[ADR 0001](../../docs/adr/0001-usar-supabase-postgresql.md) y se ejecuta en la
historia 001-005.

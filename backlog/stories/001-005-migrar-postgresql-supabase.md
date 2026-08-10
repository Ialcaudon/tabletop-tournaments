# Story 001-005: Migrar la persistencia a PostgreSQL en Supabase

**Descripción:** Reemplazar SQL Server por PostgreSQL, administrado por Supabase
en producción, manteniendo Entity Framework Core como ORM y la API como único
punto de acceso de la aplicación a la base de datos.

**Prioridad:** Alta

**Estado:** IN_PROGRESS

**Decisión arquitectónica:**
[ADR 0001](../../docs/adr/0001-usar-supabase-postgresql.md)

**Tareas:**

- [x] Definir la estrategia de acceso, migraciones y seguridad en un ADR.
- [x] Inicializar la configuración local de Supabase.
- [x] Crear la migración SQL inicial para `players`, `tournaments` y su relación.
- [x] Sustituir el proveedor EF Core de SQL Server por Npgsql.
- [x] Mapear explícitamente el esquema y los nombres PostgreSQL.
- [x] Retirar las migraciones y configuración específicas de SQL Server.
- [x] Configurar secretos y cadenas de conexión por entorno.
- [x] Migrar las pruebas de integración a PostgreSQL.
- [ ] Verificar las migraciones desde una base vacía en local y CI.
- [ ] Validar el esquema y ejecutar advisors en un proyecto de desarrollo de
  Supabase antes de desplegarlo en producción.
- [ ] Confirmar si existe información en SQL Server que deba transferirse.
- [ ] Documentar, si procede, un proceso separado de migración de datos.

La migración se ha verificado localmente sobre una base PostgreSQL vacía y el
workflow de CI ejecuta la misma suite. Falta observar una ejecución verde del
workflow antes de marcar la verificación en CI. El único proyecto Supabase
conectado tiene un nombre genérico y está inactivo, por lo que no se ha asumido
que sea un entorno de desarrollo ni se ha modificado.

**Fuera de alcance:**

- Acceso directo desde Blazor mediante la Data API.
- Supabase Auth, Storage, Realtime o Edge Functions.
- Políticas de autorización por usuario, hasta que se defina el modelo de
  autenticación.

**Requisitos previos:**

- Docker disponible para desarrollo y pruebas locales.
- Supabase CLI para crear y verificar migraciones.
- Proyecto de desarrollo de Supabase antes de validar cambios remotos.

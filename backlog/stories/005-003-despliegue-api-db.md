# Story 005-003: Planificar despliegue de API y DB

**Descripción:** Planificar despliegue de API y DB (e.g., App Service + SQL Database).  


**Prioridad:** ALTA  

**Estado:** IN_PROGRESS

**Tareas:**
- Setear conexion a base de datos en appsettings para producción.
- Setar variables de entorno para conexión a base de datos en el entorno de producción.
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection
- [ ] Obtener de Supabase la cadena de conexión PostgreSQL para producción y
  almacenarla como secreto del proveedor que aloje la API.
- [ ] Usar conexión directa para migraciones y elegir conexión directa o el
  pooler de sesión para la API según la conectividad IPv6 del proveedor elegido.
- [ ] Aplicar las migraciones como un paso explícito del despliegue; la API no
  migrará la base de datos durante el arranque.
- [ ] Incorporar el proveedor seleccionado en la historia 005-001.
- Renombrado repositories en el proyecto de backend. Ya no es un InmemoryRepository, debemos utilizar el que levantamos con docker

La estrategia de base de datos está definida en
[ADR 0001](../../docs/adr/0001-usar-supabase-postgresql.md). La implementación se
realiza en la historia 001-005.

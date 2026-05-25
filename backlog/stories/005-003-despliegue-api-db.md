# Story 005-003: Planificar despliegue de API y DB

**Descripción:** Planificar despliegue de API y DB (e.g., App Service + SQL Database).  


**Prioridad:** ALTA  

**Estado:** DONE  

**Tasks:**
- Setear conexion a base de datos en appsettings para producción.
- Setar variables de entorno para conexión a base de datos en el entorno de producción.
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection
- La cadena de conexion la extraemos de supabase que es el servicio de base de datos que vamos a usar en producción.
- Usermos postgre SQL, debemos hacer las migraciones necesarias en docker para que nos funcione en local y en producción.
- Setear migraciones de base de datos en el arranque de la aplicacion
- Ya esta configurado Render como proveedor cloud
- Renombrado repositories en el proyecto de backend. Ya no es un InmemoryRepository, debemos utilizar el que levantamos con docker

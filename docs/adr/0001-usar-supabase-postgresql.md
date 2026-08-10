# ADR 0001: Usar Supabase PostgreSQL como plataforma de base de datos

- **Estado:** Aceptado
- **Fecha:** 2026-08-10
- **Responsables:** Mantenedores de Tabletop Tournaments

## Contexto

La aplicación persiste actualmente torneos y jugadores en SQL Server mediante
Entity Framework Core. El desarrollo local, las pruebas de integración, la
configuración y la migración existente de EF Core están vinculados a SQL Server.

La producción utilizará Supabase como plataforma de base de datos. Supabase
proporciona PostgreSQL gestionado y no admite SQL Server, por lo que el proveedor
y la migración actuales no se pueden utilizar en el entorno objetivo. La elección
del proveedor que alojará la aplicación es una decisión independiente y se
realiza en la historia
[005-001](../../backlog/stories/005-001-elegir-cloud.md).

La aplicación ya dispone de una API de backend entre Blazor y la base de datos.
Actualmente no es necesario que Blazor acceda directamente a la API de datos de
Supabase.

## Decisión

### Acceso desde la aplicación

- Supabase PostgreSQL sustituirá a SQL Server en todos los entornos.
- Blazor continuará comunicándose con la API de ASP.NET Core.
- La API seguirá siendo el único componente de la aplicación con credenciales de
  base de datos.
- Entity Framework Core seguirá siendo el ORM y utilizará el proveedor Npgsql.
- Las bibliotecas cliente de Supabase, la API de datos, Auth, Storage, Realtime y
  Edge Functions quedan fuera del alcance de esta migración.

### Propiedad del esquema y migraciones

- Los archivos SQL versionados en `supabase/migrations` serán la única fuente de
  verdad del esquema de base de datos.
- Los nuevos archivos de migración se crearán con la CLI de Supabase. No se
  mantendrán migraciones de EF Core en paralelo.
- Las migraciones se aplicarán como un paso explícito del despliegue. La API no
  migrará la base de datos automáticamente durante el arranque.
- Las bases de datos locales y de pruebas de integración se crearán aplicando las
  mismas migraciones utilizadas en los entornos desplegados. `EnsureCreated` no
  sustituirá la verificación de migraciones.

### Esquema y límite de seguridad

- Las tablas de la aplicación estarán en un esquema privado `tabletop` en lugar
  del esquema expuesto `public`.
- La API de datos de Supabase deberá permanecer deshabilitada mientras la
  aplicación no la utilice.
- Las credenciales de base de datos y los secretos del proyecto se proporcionarán
  mediante variables de entorno o un gestor de secretos. Nunca se guardarán en
  el repositorio ni se enviarán a clientes Blazor.
- La seguridad a nivel de fila (RLS) se considerará una defensa adicional para
  las tablas de la aplicación. Si posteriormente se introduce la API de datos,
  los permisos y las políticas RLS deberán diseñarse y revisarse antes de exponer
  cualquier esquema.

### Conexiones

- Las migraciones de esquema y las operaciones administrativas utilizarán una
  conexión directa a PostgreSQL cuando el entorno de ejecución lo permita.
- Un despliegue persistente de la API podrá usar la conexión directa cuando haya
  IPv6. En un host que sólo disponga de IPv4 utilizará el pooler de Supavisor en
  modo sesión.
- El modo transacción del pooler no será la opción predeterminada para la API
  persistente: tiene una semántica distinta para las sentencias preparadas y está
  orientado principalmente a cargas efímeras o serverless.

## Consecuencias

### Ventajas

- Desarrollo local, pruebas y producción utilizarán el mismo motor de base de
  datos.
- Los cambios de esquema tendrán un único historial de migraciones revisable.
- Se conservarán la API y la arquitectura de dominio actuales.
- Mantener las tablas fuera de esquemas expuestos reduce la superficie accidental
  de la API de datos.

### Costes y compromisos

- Se deberán sustituir los paquetes, la configuración, los contenedores, las
  migraciones y los fixtures de pruebas específicos de SQL Server.
- Los mapeos de EF Core deberán coincidir explícitamente con el esquema y las
  convenciones de nombres de PostgreSQL.
- Los desarrolladores necesitarán Docker y la CLI de Supabase para utilizar el
  flujo local completo.
- Los despliegues necesitarán un paso separado y ordenado para aplicar el esquema.

## Plan de transición

1. Añadir una configuración local reproducible de Supabase y el esquema inicial
   de PostgreSQL.
2. Sustituir el proveedor SQL Server de EF Core por Npgsql y alinear los mapeos.
3. Mover secretos y cadenas de conexión a configuración específica por entorno.
4. Ejecutar las pruebas de integración sobre PostgreSQL utilizando las
   migraciones versionadas.
5. Validar la migración en una base local y en un proyecto de desarrollo de
   Supabase.
6. Aplicar la migración en producción sólo después de validarla y revisar las
   copias de seguridad.

## Decisiones pendientes

- Confirmar si existe una base SQL Server con datos que deban transferirse. En
  ese caso se creará un proceso separado y repetible para migrar los datos; las
  migraciones de esquema no incorporarán acceso a la base antigua.
- Decidir si la fecha de un torneo es un día de calendario (`date` / `DateOnly`)
  o un instante (`timestamptz` / UTC) antes de crear el esquema inicial.
- Confirmar las capacidades de red del proveedor que aloje la API antes de
  seleccionar el endpoint de conexión de producción.

## Referencias

- [Supabase: conexión a PostgreSQL](https://supabase.com/docs/guides/database/connecting-to-postgres)
- [Supabase: desarrollo local y migraciones](https://supabase.com/docs/guides/local-development/overview)
- [Supabase: protección de la API de datos](https://supabase.com/docs/guides/api/securing-your-api)

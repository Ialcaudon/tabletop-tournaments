# Arquitectura de Tabletop Tournaments

## Registros de decisiones arquitectónicas

- [ADR 0001: Usar Supabase PostgreSQL como plataforma de base de datos](adr/0001-usar-supabase-postgresql.md)

La implementación actual todavía utiliza SQL Server. El ADR 0001 define la
arquitectura objetivo y el plan de transición a Supabase PostgreSQL. Este trabajo
se realiza por etapas para no presentar decisiones previstas como si ya
estuvieran desplegadas.

## Visión general

El proyecto aplica una arquitectura de dominio simplificada sobre .NET 9. El
sistema permite crear y administrar torneos, jugadores y sus inscripciones.

```text
Blazor Web ──HTTP──> API ──> Application ──> Core
                         └──> Infrastructure ──> Base de datos
                                      └───────> Core
```

## Capas

- **Core:** contiene las entidades de dominio, el enum de sistemas de juego y
  las interfaces de repositorio. No depende de otros proyectos de la solución.
- **Application:** implementa los casos de uso mediante servicios de aplicación
  que dependen de las interfaces definidas en Core.
- **Infrastructure:** implementa los repositorios y el `DbContext` mediante
  Entity Framework Core. Actualmente usa SQL Server y migrará a Npgsql y
  PostgreSQL.
- **API:** expone controladores ASP.NET Core y actúa como raíz de composición para
  registrar servicios, repositorios y persistencia.
- **Web:** es una aplicación Blazor con componentes interactivos de servidor.
  Consume la API mediante clientes `HttpClient` tipados y no referencia los
  proyectos internos del backend.

## Proyectos de pruebas

- **UnitTests:** prueba entidades y servicios con xUnit, Moq y
  FluentAssertions. Los repositorios se sustituyen por mocks.
- **IntegrationTests:** prueba directamente los repositorios de infraestructura
  contra SQL Server mediante Testcontainers. Todavía no realiza pruebas HTTP de
  extremo a extremo.

Como parte de la transición a Supabase, las pruebas de integración pasarán a
PostgreSQL y aplicarán las migraciones SQL versionadas en lugar de utilizar
`EnsureCreated`.

## Patrones principales

- Las entidades protegen sus propiedades con setters privados y validan sus
  invariantes en constructores y métodos de dominio.
- Los servicios de aplicación coordinan entidades y repositorios.
- Las interfaces de repositorio se encuentran en Core y sus implementaciones en
  Infrastructure.
- Los controladores traducen peticiones HTTP a llamadas de servicios de
  aplicación.
- Blazor utiliza DTO propios y se comunica con el backend exclusivamente por
  HTTP.

## Estado de la persistencia

La configuración vigente registra `TabletopTournamentsDbContext` con SQL Server.
Los repositorios EF están activos; las implementaciones InMemory permanecen en el
repositorio, pero no están registradas en la aplicación.

La arquitectura objetivo conservará EF Core como ORM y cambiará el proveedor a
Npgsql. El esquema se versionará en `supabase/migrations` y la API será el único
componente con acceso directo a PostgreSQL.

## Ejecución actual

Desde la raíz del repositorio se pueden iniciar API y Blazor con:

```bash
./start-dev.sh
```

Los endpoints locales actuales son:

- API y Swagger: `http://localhost:5102/swagger`
- Blazor: `http://localhost:5067`

La base de datos y las instrucciones de ejecución cambiarán en los siguientes
bloques de la migración a PostgreSQL.

## Pruebas actuales

Las suites pueden ejecutarse por separado:

```bash
dotnet test tests/TabletopTournaments.UnitTests
dotnet test tests/TabletopTournaments.IntegrationTests
```

Las pruebas de integración requieren Docker porque levantan un contenedor de
base de datos mediante Testcontainers.

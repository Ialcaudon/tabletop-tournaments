# Arquitectura de Tabletop Tournaments

## Registros de decisiones arquitectónicas

- [ADR 0001: Usar Supabase PostgreSQL como plataforma de base de datos](adr/0001-usar-supabase-postgresql.md)

La persistencia utiliza PostgreSQL mediante Npgsql. El ADR 0001 define la
estrategia para desarrollar en local y desplegar la base de datos administrada
por Supabase.

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
  Entity Framework Core y Npgsql.
- **API:** expone controladores ASP.NET Core y actúa como raíz de composición para
  registrar servicios, repositorios y persistencia.
- **Web:** es una aplicación Blazor con componentes interactivos de servidor.
  Consume la API mediante clientes `HttpClient` tipados y no referencia los
  proyectos internos del backend.

## Proyectos de pruebas

- **UnitTests:** prueba entidades y servicios con xUnit, Moq y
  FluentAssertions. Los repositorios se sustituyen por mocks.
- **IntegrationTests:** prueba directamente los repositorios de infraestructura
  contra PostgreSQL mediante Testcontainers. Cada fixture parte de una base
  vacía y aplica en orden las migraciones de `supabase/migrations`; no utiliza
  `EnsureCreated`. Todavía no realiza pruebas HTTP de extremo a extremo.

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

La configuración registra `TabletopTournamentsDbContext` con Npgsql. Los
repositorios EF están activos; las implementaciones InMemory permanecen en el
repositorio, pero no están registradas en la aplicación.

El esquema privado `tabletop` y sus nombres `snake_case` se mapean explícitamente
en Infrastructure. `supabase/migrations` es la única fuente de verdad del
esquema y la API es el único componente con acceso directo a PostgreSQL. La fecha
de un torneo es un día de calendario (`DateOnly` / `date`).

## Desarrollo local

La CLI de Supabase inicia PostgreSQL y aplica las migraciones versionadas:

```bash
supabase start
supabase db reset
```

La API obtiene la conexión exclusivamente de
`ConnectionStrings__DefaultConnection`. `start-dev.sh` carga esta variable desde
el archivo local `.env`, que no se versiona. `.env.example` documenta el formato
sin incluir credenciales reales.

Después de preparar PostgreSQL y `.env`, se pueden iniciar API y Blazor desde la
raíz del repositorio con:

```bash
./start-dev.sh
```

Los endpoints locales actuales son:

- API y Swagger: `http://localhost:5102/swagger`
- Blazor: `http://localhost:5067`

En Supabase alojado, las migraciones y las tareas administrativas usan la
conexión directa. La API persistente usa también la conexión directa si dispone
de IPv6 y Supavisor en modo sesión cuando el host sólo tiene IPv4.

## Pruebas actuales

Las suites pueden ejecutarse por separado:

```bash
dotnet test tests/TabletopTournaments.UnitTests
dotnet test tests/TabletopTournaments.IntegrationTests
```

Las pruebas de integración requieren Docker porque levantan un contenedor de
base de datos mediante Testcontainers.

# Tabletop Tournaments

Aplicación para gestionar torneos de juegos de mesa, formada por una API de
ASP.NET Core y un frontend Blazor Web App con componentes interactivos de
servidor.

## Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/) para la base de datos local y las pruebas de
  integración
- [Supabase CLI](https://supabase.com/docs/guides/local-development/cli/getting-started)
  para iniciar PostgreSQL local y aplicar las migraciones

## Primeros pasos

### 1. Clonar el repositorio

```bash
git clone https://github.com/Ialcaudon/tabletop-tournaments.git
cd tabletop-tournaments
```

### 2. Compilar la solución

```bash
dotnet build
```

### 3. Preparar PostgreSQL local

Inicia el entorno local y reconstruye la base desde las migraciones versionadas:

```bash
supabase start
supabase db reset
```

Crea el archivo local de configuración de entorno y completa la contraseña que
muestra la CLI. `.env` está excluido de Git:

```bash
cp .env.example .env
```

`supabase/migrations` es la única fuente de verdad del esquema. No uses
`EnsureCreated` ni generes migraciones de Entity Framework Core.

### 4. Arrancar la API y el frontend

La forma más sencilla de iniciar ambos proyectos desde la raíz del repositorio
es:

```bash
./start-dev.sh
```

Los servicios quedan disponibles en:

- Frontend Blazor: `http://localhost:5067`
- API y Swagger: `http://localhost:5102/swagger`

Pulsa `Ctrl+C` en la terminal para detener ambos procesos.

Para detener los servicios locales de Supabase cuando termines:

```bash
supabase stop
```

## Configuración de Supabase alojado

Configura `ConnectionStrings__DefaultConnection` en las variables de entorno o
el gestor de secretos del proveedor de la API. Usa la conexión directa para
migraciones y para una API persistente con IPv6. Si el host sólo admite IPv4,
usa Supavisor en modo sesión (puerto `5432`). Las conexiones remotas deben exigir
TLS. No expongas esta cadena al proyecto Blazor.

Valida primero las migraciones sobre un proyecto de desarrollo identificado de
forma inequívoca:

```bash
supabase link --project-ref <project-ref-de-desarrollo>
supabase db push --dry-run
supabase db push
supabase migration list
```

Después del despliegue revisa los advisors de seguridad y rendimiento en
Supabase. No ejecutes estos comandos contra producción hasta haber confirmado si
la base SQL Server anterior contiene datos que deban transferirse.

## Arrancar el frontend por separado

El frontend consume la API por HTTP, por lo que para utilizar todas sus
funcionalidades la API también debe estar en ejecución. Inicia cada proyecto en
una terminal distinta.

Terminal 1, API:

```bash
dotnet run --project src/TabletopTournaments.API/TabletopTournaments.API.csproj --launch-profile http
```

Terminal 2, frontend:

```bash
dotnet run --project src/TabletopTournaments.Web/TabletopTournaments.Web.csproj --launch-profile http
```

En el entorno de desarrollo, el frontend utiliza la dirección de la API definida
en `src/TabletopTournaments.Web/appsettings.Development.json` y se abre en
`http://localhost:5067`.

## Estructura del proyecto

- **Core:** entidades de dominio e interfaces de repositorio.
- **Application:** casos de uso y servicios de aplicación.
- **Infrastructure:** persistencia e implementaciones de los repositorios.
- **API:** backend ASP.NET Core y raíz de composición.
- **Web:** frontend Blazor que consume la API mediante clientes `HttpClient`
  tipados.
- **UnitTests:** pruebas unitarias de Core y Application.
- **IntegrationTests:** pruebas de persistencia PostgreSQL mediante Testcontainers
  que aplican las migraciones SQL sobre una base vacía.

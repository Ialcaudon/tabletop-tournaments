# Tabletop Tournaments

Aplicación para gestionar torneos de juegos de mesa, formada por una API de
ASP.NET Core y un frontend Blazor Web App con componentes interactivos de
servidor.

## Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/) para la base de datos local y las pruebas de
  integración

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

### 3. Arrancar la API y el frontend

La forma más sencilla de iniciar ambos proyectos desde la raíz del repositorio
es:

```bash
./start-dev.sh
```

Los servicios quedan disponibles en:

- Frontend Blazor: `http://localhost:5067`
- API y Swagger: `http://localhost:5102/swagger`

Pulsa `Ctrl+C` en la terminal para detener ambos procesos.

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
- **IntegrationTests:** pruebas de persistencia mediante Testcontainers.

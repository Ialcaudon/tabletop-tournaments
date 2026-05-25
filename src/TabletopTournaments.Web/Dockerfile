FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TabletopTournaments.Web/TabletopTournaments.Web.csproj", "src/TabletopTournaments.Web/"]
RUN dotnet restore "src/TabletopTournaments.Web/TabletopTournaments.Web.csproj"
COPY . .
WORKDIR "/src/src/TabletopTournaments.Web"
RUN dotnet build "./TabletopTournaments.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TabletopTournaments.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TabletopTournaments.Web.dll"]

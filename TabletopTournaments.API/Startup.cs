using TabletopTournaments.Application.Services;
using TabletopTournaments.Core.Interfaces;
using TabletopTournaments.Infrastructure.Repositories;

namespace TabletopTournaments.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método se llama en tiempo de ejecución y se utiliza para agregar servicios al contenedor.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        // Repositories (Singleton for InMemory persistence)
        services.AddSingleton<ITournamentRepository, InMemoryTournamentRepository>();
        services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();

        // Application Services
        services.AddTransient<ITournamentService, TournamentService>();
        services.AddTransient<IPlayerService, PlayerService>();
    }

    // Este método se llama en tiempo de ejecución y se utiliza para configurar la canalización de solicitudes HTTP.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();
    }
}

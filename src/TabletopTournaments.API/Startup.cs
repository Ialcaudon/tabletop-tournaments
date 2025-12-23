using TabletopTournaments.Application.Services;
using TabletopTournaments.Core.Interfaces;
using TabletopTournaments.Infrastructure.Repositories;
using TabletopTournaments.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace TabletopTournaments.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        // DbContext
        services.AddDbContext<TabletopTournamentsDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        // Application Services
        services.AddTransient<ITournamentService, TournamentService>();
        services.AddTransient<IPlayerService, PlayerService>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
    }
}

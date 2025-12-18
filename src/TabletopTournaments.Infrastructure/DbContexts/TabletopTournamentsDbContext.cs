using Microsoft.EntityFrameworkCore;
using TabletopTournaments.Core.Entities;

namespace TabletopTournaments.Infrastructure.DbContexts
{
    public class TabletopTournamentsDbContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public TabletopTournamentsDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TabletopTournamentsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

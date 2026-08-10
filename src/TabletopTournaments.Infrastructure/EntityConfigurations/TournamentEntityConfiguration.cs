using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabletopTournaments.Core.Entities;


namespace TabletopTournaments.Infrastructure.EntityConfigurations
{
    internal class TournamentEntityConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("tournaments", "tabletop");

            builder.HasKey(x => x.Id)
                .HasName("pk_tournaments");
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Date)
                .HasColumnName("date")
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.GameSystem)
                .HasColumnName("game_system")
                .HasConversion<int>()
                .IsRequired();

            builder.HasMany(x => x.Players)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TournamentPlayer",
                    right => right.HasOne<Player>()
                        .WithMany()
                        .HasForeignKey("player_id")
                        .HasConstraintName("fk_tournament_players_player")
                        .OnDelete(DeleteBehavior.Cascade),
                    left => left.HasOne<Tournament>()
                        .WithMany()
                        .HasForeignKey("tournament_id")
                        .HasConstraintName("fk_tournament_players_tournament")
                        .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("tournament_players", "tabletop");
                        join.HasKey("tournament_id", "player_id")
                            .HasName("pk_tournament_players");
                        join.HasIndex("player_id")
                            .HasDatabaseName("ix_tournament_players_player_id");
                    });
        }
    }
}

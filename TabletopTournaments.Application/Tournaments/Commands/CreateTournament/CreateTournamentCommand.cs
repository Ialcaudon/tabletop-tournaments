using System;
using TabletopTournaments.Core.Enums;

namespace TabletopTournaments.Application.Tournaments.Commands.CreateTournament
{
    public class CreateTournamentCommand
    {
        public string Name { get; }
        public DateTime Date { get; }
        public GameSystem GameSystem { get; }

        public CreateTournamentCommand(string name, DateTime date, GameSystem gameSystem)
        {
            Name = name;
            Date = date;
            GameSystem = gameSystem;
        }
    }
}

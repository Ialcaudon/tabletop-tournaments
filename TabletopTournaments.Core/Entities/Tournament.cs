using System;
using System.Collections.Generic;
using TabletopTournaments.Core.Enums;

namespace TabletopTournaments.Core.Entities
{
    public class Tournament
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public GameSystem GameSystem { get; private set; }

        // EF Core Constructor
        protected Tournament() { }

        public Tournament(string name, DateTime date, GameSystem gameSystem)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tournament name cannot be empty", nameof(name));

            Name = name;
            Date = date;
            GameSystem = gameSystem;
        }
    }
}

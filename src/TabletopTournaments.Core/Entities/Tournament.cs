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

        private readonly List<Player> _players = new();
        public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

        protected Tournament() { }

        public Tournament(string name, DateTime date, GameSystem gameSystem)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tournament name cannot be empty", nameof(name));

            Name = name;
            Date = date;
            GameSystem = gameSystem;
        }

        public void Update(string name, DateTime date, GameSystem gameSystem)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tournament name cannot be empty", nameof(name));

            Name = name;
            Date = date;
            GameSystem = gameSystem;
        }

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (_players.Contains(player))
                throw new InvalidOperationException("Player is already registered in this tournament");

            _players.Add(player);
        }
    }
}

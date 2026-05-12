using System;

namespace TabletopTournaments.Core.Entities
{
    public class Player
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Player() { }

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Player name cannot be empty", nameof(name));

            Name = name;
        }
    }
}

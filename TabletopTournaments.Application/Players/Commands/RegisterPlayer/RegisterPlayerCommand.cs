namespace TabletopTournaments.Application.Players.Commands.RegisterPlayer
{
    public class RegisterPlayerCommand
    {
        public string Name { get; }

        public RegisterPlayerCommand(string name)
        {
            Name = name;
        }
    }
}

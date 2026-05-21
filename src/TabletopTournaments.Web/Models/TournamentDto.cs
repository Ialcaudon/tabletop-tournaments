namespace TabletopTournaments.Web.Models;

public class TournamentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int GameSystem { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
}

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateTournamentModel
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now.AddDays(7);
    public int GameSystem { get; set; }
}

public class RegisterPlayerModel
{
    public string Name { get; set; } = string.Empty;
}


namespace StarJetAirways.API.Configuration.Settings;

public class Neo4jConnectionSettings
{
    public required string Uri { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}


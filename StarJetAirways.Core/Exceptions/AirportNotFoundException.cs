namespace StarJetAirways.Core.Exceptions;

public class AirportNotFoundException : Exception
{
    public AirportNotFoundException() : base() { }
    public AirportNotFoundException(string message) : base(message) { }
    public AirportNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

namespace StarJetAirways.Core.Exceptions;

public class AircraftNotFoundException : Exception
{
    public AircraftNotFoundException() : base() { }
    public AircraftNotFoundException(string message) : base(message) { }
    public AircraftNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

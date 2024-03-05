namespace StarJetAirways.Core.Exceptions;

public class AirlineNotFoundException : Exception
{
    public AirlineNotFoundException() : base() { }
    public AirlineNotFoundException(string message) : base(message) { }
    public AirlineNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

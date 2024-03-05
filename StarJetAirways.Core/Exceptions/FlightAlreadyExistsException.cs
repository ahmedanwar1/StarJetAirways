namespace StarJetAirways.Core.Exceptions;

public class FlightAlreadyExistsException : Exception
{
    public FlightAlreadyExistsException() : base() { }
    public FlightAlreadyExistsException(string message) : base(message) { }
    public FlightAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}

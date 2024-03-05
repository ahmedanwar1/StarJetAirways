namespace StarJetAirways.Core.Exceptions;

public class InvalidAirportCodeException : Exception
{
    public InvalidAirportCodeException() : base() { }
    public InvalidAirportCodeException(string message) : base(message) { }
    public InvalidAirportCodeException(string message, Exception innerException) : base(message, innerException) { }
}

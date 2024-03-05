
namespace StarJetAirways.Core.Exceptions;

public class InvalidAirlineIdException : Exception
{
    public InvalidAirlineIdException() : base() { }
    public InvalidAirlineIdException(string message) : base(message) { }
    public InvalidAirlineIdException(string message, Exception innerException) : base(message, innerException) { }
}

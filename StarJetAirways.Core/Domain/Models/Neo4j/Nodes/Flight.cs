using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Flight : IValidatableObject
{
    [Key]
    public Guid FlightID { get; set; }

    [Required]
    public DateTime DepartureDateTime { get; set; }

    [Required]
    public DateTime ArrivalDateTime { get; set; }

    [Range(0, int.MaxValue)]
    public int AvailableFirstClassSeats { get; set; } = 0;

    [Range(0, int.MaxValue)]
    public int AvailableBusinessClassSeats { get; set; } = 0;

    [Range(0, int.MaxValue)]
    public int AvailableEconomyClassSeats { get; set; } = 0;

    [Range(0, double.MaxValue)]
    public decimal FirstClassPrice { get; set; }

    [Range(0, double.MaxValue)]
    public decimal BusinessClassPrice { get; set; }

    [Range(0, double.MaxValue)]
    public decimal EconomyClassPrice { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartureDateTime >= ArrivalDateTime)
        {
            yield return new ValidationResult("Departure date must be before Arrival date.");
        }
    }
}

/*

Flight:

AirlineID (Foreign Key referencing Airline.AirlineID)
AircraftID (Foreign Key referencing Plane.PlaneID)
DepartureAirportCode (Foreign Key referencing Airport.AirportCode)
DestinationAirportCode (Foreign Key referencing Airport.AirportCode)

... 
 
*/
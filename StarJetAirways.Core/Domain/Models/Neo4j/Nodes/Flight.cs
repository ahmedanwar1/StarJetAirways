using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Flight : IValidatableObject
{
    [Key]
    [JsonProperty(PropertyName = "flightID")]
    public Guid FlightID { get; set; }

    [Required]
    [JsonProperty(PropertyName = "departureDateTime")]
    public DateTime DepartureDateTime { get; set; }

    [Required]
    [JsonProperty(PropertyName = "arrivalDateTime")]
    public DateTime ArrivalDateTime { get; set; }

    [Range(0, int.MaxValue)]
    [JsonProperty(PropertyName = "availableFirstClassSeats")]
    public int AvailableFirstClassSeats { get; set; } = 0;

    [Range(0, int.MaxValue)]
    [JsonProperty(PropertyName = "availableBusinessClassSeats")]
    public int AvailableBusinessClassSeats { get; set; } = 0;

    [Range(0, int.MaxValue)]
    [JsonProperty(PropertyName = "availableEconomyClassSeats")]
    public int AvailableEconomyClassSeats { get; set; } = 0;

    [Range(0, double.MaxValue)]
    [JsonProperty(PropertyName = "firstClassPrice")]
    public decimal FirstClassPrice { get; set; }

    [Range(0, double.MaxValue)]
    [JsonProperty(PropertyName = "businessClassPrice")]
    public decimal BusinessClassPrice { get; set; }

    [Range(0, double.MaxValue)]
    [JsonProperty(PropertyName = "economyClassPrice")]
    public decimal EconomyClassPrice { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartureDateTime >= ArrivalDateTime)
        {
            yield return new ValidationResult("Departure date must be before Arrival date.");
        }
    }
}

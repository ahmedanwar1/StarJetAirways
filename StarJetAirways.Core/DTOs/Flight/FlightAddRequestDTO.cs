using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class FlightAddRequestDTO : IValidatableObject
{

    [JsonProperty(PropertyName = "departureDateTime")]
    [Required]
    public DateTime DepartureDateTime { get; set; }

    [JsonProperty(PropertyName = "arrivalDateTime")]
    [Required]
    public DateTime ArrivalDateTime { get; set; }

    [JsonProperty(PropertyName = "availableFirstClassSeats")]
    [Range(0, int.MaxValue)]
    public int AvailableFirstClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "availableBusinessClassSeats")]
    [Range(0, int.MaxValue)]
    public int AvailableBusinessClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "availableEconomyClassSeats")]
    [Range(0, int.MaxValue)]
    public int AvailableEconomyClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "firstClassPrice")]
    [Range(0, double.MaxValue)]
    public decimal FirstClassPrice { get; set; }

    [JsonProperty(PropertyName = "businessClassPrice")]
    [Range(0, double.MaxValue)]
    public decimal BusinessClassPrice { get; set; }

    [JsonProperty(PropertyName = "economyClassPrice")]
    [Range(0, double.MaxValue)]
    public decimal EconomyClassPrice { get; set; }

    [JsonProperty(PropertyName = "airlineId")]
    public Guid AirlineId { get; set; }

    [JsonProperty(PropertyName = "aircraftId")]
    public Guid AircraftId { get; set; }

    [JsonProperty(PropertyName = "departureAirportCode")]
    public required string DepartureAirportCode { get; set; }

    [JsonProperty(PropertyName = "arrivalAirportCode")]
    public required string ArrivalAirportCode { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartureDateTime >= ArrivalDateTime)
        {
            yield return new ValidationResult("Departure date must be before Arrival date.");
        }

        if (String.Equals(DepartureAirportCode, ArrivalAirportCode, StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult("Departure and arrival airports cannot be the same.");
        }
    }


}

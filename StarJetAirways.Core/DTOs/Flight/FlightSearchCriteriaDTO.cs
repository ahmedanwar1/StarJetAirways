using StarJetAirways.Core.Domain.CustomValidators;
using StarJetAirways.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class FlightSearchCriteriaDTO : IValidatableObject
{
    [Required]
    public required string DepartureAirportCode { get; set; }

    [Required]
    public required string ArrivalAirportCode { get; set; }

    [Required]
    public DateTime? DepartureDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PassengerCount { get; set; }
    public TravelClassEnum TravelClass { get; set; } = TravelClassEnum.All;
    public bool DirectFlightOnly { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReturnDate != null && ReturnDate < DepartureDate)
        {
            yield return new ValidationResult("Departure date must be before Return date.");
        }
    }
}

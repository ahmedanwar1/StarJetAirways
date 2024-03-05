using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AirportAddRequestDTO
{
    [Alphabetic]
    [JsonProperty(PropertyName = "airportCode")]
    public required string AirportCode { get; set; }

    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [JsonProperty(PropertyName = "airportName")]
    public required string AirportName { get; set; }

    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [JsonProperty(PropertyName = "city")]
    public required string City { get; set; }

    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [JsonProperty(PropertyName = "country")]
    public required string Country { get; set; }

}

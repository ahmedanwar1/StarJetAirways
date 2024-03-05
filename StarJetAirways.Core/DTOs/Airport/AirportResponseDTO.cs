using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AirportResponseDTO
{
    [JsonProperty(PropertyName = "airportCode")]
    public required string AirportCode { get; set; }

    [JsonProperty(PropertyName = "airportName")]
    public required string AirportName { get; set; }

    [JsonProperty(PropertyName = "city")]
    public required string City { get; set; }

    [JsonProperty(PropertyName = "country")]
    public required string Country { get; set; }
}

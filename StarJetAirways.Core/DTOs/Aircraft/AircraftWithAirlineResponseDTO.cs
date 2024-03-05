using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AircraftWithAirlineResponseDTO
{
    [JsonProperty(PropertyName = "aircraftId")]
    public Guid AircraftID { get; set; }

    [JsonProperty(PropertyName = "aircraftType")]
    public required string AircraftType { get; set; }

    [JsonProperty(PropertyName = "totalFirstClassSeats")]
    public int TotalFirstClassSeats { get; set; }

    [JsonProperty(PropertyName = "totalBusinessClassSeats")]
    public int TotalBusinessClassSeats { get; set; }

    [JsonProperty(PropertyName = "totalEconomyClassSeats")]
    public int TotalEconomyClassSeats { get; set; }

    [JsonProperty(PropertyName = "airline")]
    public required AirlineResponseDTO Airline { get; set; }
}

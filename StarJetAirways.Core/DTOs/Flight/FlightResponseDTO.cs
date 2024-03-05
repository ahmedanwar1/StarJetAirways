using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class FlightResponseDTO
{
    [JsonProperty(PropertyName = "flightID")]
    public Guid FlightID { get; set; }

    [JsonProperty(PropertyName = "departureDateTime")]
    public DateTime DepartureDateTime { get; set; }

    [JsonProperty(PropertyName = "arrivalDateTime")]
    public DateTime ArrivalDateTime { get; set; }

    [JsonProperty(PropertyName = "availableFirstClassSeats")]
    public int AvailableFirstClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "availableBusinessClassSeats")]
    public int AvailableBusinessClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "availableEconomyClassSeats")]
    public int AvailableEconomyClassSeats { get; set; } = 0;

    [JsonProperty(PropertyName = "firstClassPrice")]
    public decimal FirstClassPrice { get; set; }

    [JsonProperty(PropertyName = "businessClassPrice")]
    public decimal BusinessClassPrice { get; set; }

    [JsonProperty(PropertyName = "economyClassPrice")]
    public decimal EconomyClassPrice { get; set; }

}

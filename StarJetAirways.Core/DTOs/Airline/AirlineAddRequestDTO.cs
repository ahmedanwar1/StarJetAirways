using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AirlineAddRequestDTO
{
    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [MaxLength(50)]
    [JsonProperty(PropertyName = "airlineName")]
    public required string AirlineName { get; set; }
}

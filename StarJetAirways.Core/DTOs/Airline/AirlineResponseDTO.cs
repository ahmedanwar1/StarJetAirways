using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AirlineResponseDTO
{
    [Required]
    [JsonProperty(PropertyName = "airlineId")]
    public Guid AirlineId { get; set; }

    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [MaxLength(50)]
    [JsonProperty(PropertyName = "airlineName")]
    public required string AirlineName { get; set; }
}

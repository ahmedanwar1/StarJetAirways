using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Airport
{
    [Key]
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

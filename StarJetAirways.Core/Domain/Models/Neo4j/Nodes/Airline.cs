using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Airline
{
    [Key]
    [Required]
    [JsonProperty(PropertyName = "airlineId")]
    public Guid AirlineId { get; set; }

    [Required]
    [Alphabetic(SpacesAllowed = true)]
    [MaxLength(50)]
    [JsonProperty(PropertyName = "airlineName")]
    public required string AirlineName { get; set; }

    //public virtual ICollection<Aircraft> Aircrafts { get; set; } = new HashSet<Aircraft>();
}

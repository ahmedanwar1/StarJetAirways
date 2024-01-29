using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Airline
{
    [Key]
    [Required]
    public Guid AirlineId { get; set; }

    [Required]
    [Alphabetic]
    [MaxLength(50)]
    public required string AirlineName { get; set; }

    public virtual ICollection<Aircraft> Aircrafts { get; set; } = new HashSet<Aircraft>();
}

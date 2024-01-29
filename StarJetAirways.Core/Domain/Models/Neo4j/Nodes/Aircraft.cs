using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;

public class Aircraft
{
    [Key]
    public Guid AircraftID { get; set; }

    [Required]
    [Alphabetic]
    [MaxLength(50)]
    public required string AircraftType { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalFirstClassSeats { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalBusinessClassSeats { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalEconomyClassSeats { get; set; }

    [Required]
    [ForeignKey("Airline")]
    public Guid AirlineID { get; set; }

    public virtual Airline Airline { get; set; }
}

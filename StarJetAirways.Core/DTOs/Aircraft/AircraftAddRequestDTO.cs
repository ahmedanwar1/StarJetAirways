﻿using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AircraftAddRequestDTO
{
    [JsonProperty(PropertyName = "aircraftType")]
    [Required]
    [MaxLength(50)]
    public required string AircraftType { get; set; }

    [JsonProperty(PropertyName = "totalFirstClassSeats")]
    [Range(0, int.MaxValue)]
    public int TotalFirstClassSeats { get; set; }

    [JsonProperty(PropertyName = "totalBusinessClassSeats")]
    [Range(0, int.MaxValue)]
    public int TotalBusinessClassSeats { get; set; }

    [JsonProperty(PropertyName = "totalEconomyClassSeats")]
    [Range(0, int.MaxValue)]
    public int TotalEconomyClassSeats { get; set; }

    [JsonProperty(PropertyName = "airlineId")]
    public Guid AirlineId { get; set; }


}

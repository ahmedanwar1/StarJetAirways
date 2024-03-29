﻿using Newtonsoft.Json;
using StarJetAirways.Core.Domain.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.DTOs;

public class AirlineResponseDTO
{
    [JsonProperty(PropertyName = "airlineId")]
    public Guid AirlineId { get; set; }

    [JsonProperty(PropertyName = "airlineName")]
    public required string AirlineName { get; set; }
}

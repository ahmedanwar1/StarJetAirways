using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Mappers;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<AirportAddRequestDTO, Airport>();
        CreateMap<Airport, AirportResponseDTO>();
    }
}

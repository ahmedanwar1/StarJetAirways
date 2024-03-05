using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Mappers;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<FlightAddRequestDTO, Flight>().ForMember(d => d.FlightID, src => src.MapFrom(src => Guid.NewGuid())); ;
        CreateMap<Flight, FlightResponseDTO>();
    }
}

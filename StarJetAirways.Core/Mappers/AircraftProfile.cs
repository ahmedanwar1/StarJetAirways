using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Mappers;

public class AircraftProfile : Profile
{
    public AircraftProfile()
    {
        CreateMap<AircraftAddRequestDTO, Aircraft>()
            .ForMember(d => d.AircraftID, src => src.MapFrom(src => Guid.NewGuid()));

        CreateMap<Aircraft, AircraftResponseDTO>();
    }
}

using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Mappers;

public class AirlineProfile : Profile
{
    public AirlineProfile()
    {
        CreateMap<AirlineAddRequestDTO, Airline>()
            .ForMember(d => d.AirlineId, src => src.MapFrom(src => Guid.NewGuid()));

        CreateMap<Airline, AirlineResponseDTO>();
    }
}

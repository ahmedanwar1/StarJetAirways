using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirlinesAdderService : IAirlinesAdderService
{
    private readonly IMapper _mapper;
    private readonly IAirlinesRepository _airlinesRepository;

    public AirlinesAdderService(IAirlinesRepository airlinesRepository, IMapper mapper)
    {
        _airlinesRepository = airlinesRepository;
        _mapper = mapper;
    }

    public async Task<AirlineResponseDTO> AddAirline(AirlineAddRequestDTO airlineAddRequest)
    {
        Airline airlineModel = _mapper.Map<Airline>(airlineAddRequest);

        Airline airlineModelResult = await _airlinesRepository.AddAirline(airlineModel);

        AirlineResponseDTO airlineResponse = _mapper.Map<AirlineResponseDTO>(airlineModelResult);

        return airlineResponse;
    }
}

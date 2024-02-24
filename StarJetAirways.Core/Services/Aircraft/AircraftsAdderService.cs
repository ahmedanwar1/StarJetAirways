using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Exceptions;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AircraftsAdderService : IAircraftsAdderService
{
    private readonly IMapper _mapper;
    private readonly IAircraftsRepository _aircraftsRepository;
    private readonly IAirlinesGetterService _airlinesGetterService;

    public AircraftsAdderService(IMapper mapper, IAircraftsRepository aircraftsRepository, IAirlinesGetterService airlinesGetterService)
    {
        _mapper = mapper;
        _aircraftsRepository = aircraftsRepository;
        _airlinesGetterService = airlinesGetterService;
    }

    public async Task<AircraftResponseDTO> AddAircraft(AircraftAddRequestDTO aircraftAddRequest)
    {
        Guid airlineId = aircraftAddRequest.AirlineId;

        if (await _airlinesGetterService.CheckAirlineExistsAsync(airlineId) == false)
        {
            throw new InvalidAirlineIdException("Invalid Aircraft Id!");
        }

        //map
        Aircraft aircraftModel = _mapper.Map<Aircraft>(aircraftAddRequest);

        //create
        Aircraft airlineModelResult = await _aircraftsRepository.AddAircraft(aircraftModel, airlineId);

        //map and return
        AircraftResponseDTO aircraftResponse = _mapper.Map<AircraftResponseDTO>(airlineModelResult);

        return aircraftResponse;
    }
}

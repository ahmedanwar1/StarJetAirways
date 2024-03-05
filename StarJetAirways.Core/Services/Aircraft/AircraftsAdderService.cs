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
    private readonly IAirlinesCheckerService _airlinesCheckerService;

    public AircraftsAdderService(IMapper mapper, IAircraftsRepository aircraftsRepository, IAirlinesCheckerService airlinesCheckerService)
    {
        _mapper = mapper;
        _aircraftsRepository = aircraftsRepository;
        _airlinesCheckerService = airlinesCheckerService;
    }

    public async Task<AircraftResponseDTO> AddAircraftAsync(AircraftAddRequestDTO aircraftAddRequest)
    {
        Guid airlineId = aircraftAddRequest.AirlineId;

        if (await _airlinesCheckerService.CheckAirlineExistsAsync(airlineId) == false)
        {
            throw new InvalidAirlineIdException("Invalid Aircraft Id!");
        }

        //map
        Aircraft aircraftModel = _mapper.Map<Aircraft>(aircraftAddRequest);

        //create
        Aircraft airlineModelResult = await _aircraftsRepository.AddAircraftAsync(aircraftModel, airlineId);

        //map and return
        AircraftResponseDTO aircraftResponse = _mapper.Map<AircraftResponseDTO>(airlineModelResult);

        return aircraftResponse;
    }
}

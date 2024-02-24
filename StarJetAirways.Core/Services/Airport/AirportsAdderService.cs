using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Exceptions;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirportsAdderService : IAirportsAdderService
{
    private readonly IMapper _mapper;
    private readonly IAirportsRepository _airportsRepository;
    private readonly IAirportsGetterService _airportsGetterService;

    public AirportsAdderService(IMapper mapper, IAirportsRepository airportsRepository, IAirportsGetterService airportsGetterService)
    {
        _mapper = mapper;
        _airportsRepository = airportsRepository;
        _airportsGetterService = airportsGetterService;
    }

    public async Task<AirportResponseDTO> AddAirport(AirportAddRequestDTO airportAddRequestDto)
    {
        //check airport does not exist
        if (await _airportsGetterService.CheckAirportExistsAsync(airportAddRequestDto.AirportCode))
        {
            throw new InvalidAirportCodeException("Airport code already exists!");
        }

        //convert AddRequrstDTO to model
        Airport airportModel = _mapper.Map<Airport>(airportAddRequestDto);

        Airport airportModelResult = await _airportsRepository.AddAirport(airportModel);

        AirportResponseDTO airportResponse = _mapper.Map<AirportResponseDTO>(airportModelResult);


        return airportResponse;
    }
}

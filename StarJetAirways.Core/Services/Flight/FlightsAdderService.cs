using AutoMapper;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;
using System.Threading;

namespace StarJetAirways.Core.Services;

public class FlightsAdderService : IFlightsAdderService
{
    private readonly IMapper _mapper;
    private readonly IFlightsRepository _flightsRepository;
    private readonly IFlightsCheckerService _flightsCheckerService;

    public FlightsAdderService(
        IMapper mapper,
        IFlightsRepository flightsRepository,
        IFlightsCheckerService flightsCheckerService
        )
    {
        _mapper = mapper;
        _flightsRepository = flightsRepository;
        _flightsCheckerService = flightsCheckerService;
    }

    public async Task<FlightResponseDTO> AddFlightAsync(FlightAddRequestDTO flightAddRequest)
    {

        using (SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1))
        {
            try
            {
                await semaphoreSlim.WaitAsync();

                bool isValidForAdding = await _flightsCheckerService.IsFlightValidForAddingAsync(flightAddRequest);

                if (!isValidForAdding)
                {
                    throw new ArgumentException();
                }

                //map it to model
                Flight flightModel = _mapper.Map<Flight>(flightAddRequest);


                //add flight
                var addedFlightModel = await _flightsRepository.AddFlightAsync(
                    flightModel,
                    flightAddRequest.AirlineId,
                    flightAddRequest.AircraftId,
                    flightAddRequest.DepartureAirportCode,
                    flightAddRequest.ArrivalAirportCode
                    );

                //map and return
                FlightResponseDTO flightResponse = _mapper.Map<FlightResponseDTO>(addedFlightModel);

                return flightResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}

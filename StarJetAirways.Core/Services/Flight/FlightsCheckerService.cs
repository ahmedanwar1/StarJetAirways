using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Exceptions;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class FlightsCheckerService : IFlightsCheckerService
{
    private readonly IFlightsRepository _flightsRepository;
    private readonly IAirportsCheckerService _airportsCheckerService;
    private readonly IAirlinesCheckerService _airlinesCheckerService;
    private readonly IAircraftsCheckerService _aircraftsCheckerService;
    private readonly IAircraftsGetterService _aircraftsGetterService;

    public FlightsCheckerService(
        IFlightsRepository flightsRepository,
        IAirportsCheckerService airportsCheckerService,
        IAirlinesCheckerService airlinesCheckerService,
        IAircraftsCheckerService aircraftsCheckerService,
        IAircraftsGetterService aircraftsGetterService
        )
    {
        _flightsRepository = flightsRepository;
        _airportsCheckerService = airportsCheckerService;
        _airlinesCheckerService = airlinesCheckerService;
        _aircraftsCheckerService = aircraftsCheckerService;
        _aircraftsGetterService = aircraftsGetterService;
    }

    public async Task<bool> CheckFlightExistsAsync(Guid flightId)
    {
        var flight = await _flightsRepository.GetFlightByIdAsync(flightId);

        if (flight == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> IsFlightValidForAddingAsync(FlightAddRequestDTO flightAddRequest)
    {
        List<Task<bool>> tasks = new()
        {
            //CheckFlightExistsAsync(flightAddRequest.FlightID),
            _airportsCheckerService.CheckAirportExistsAsync(flightAddRequest.DepartureAirportCode),
            _airportsCheckerService.CheckAirportExistsAsync(flightAddRequest.ArrivalAirportCode),
            _airlinesCheckerService.CheckAirlineExistsAsync(flightAddRequest.AirlineId),
            _aircraftsCheckerService.CheckAircraftExistsAsync(flightAddRequest.AircraftId),
        };

        var tasksResult = await Task.WhenAll(tasks);

        //bool flightExists = tasksResult[0];
        bool departureAirportExists = tasksResult[0];
        bool arrivalAirportExists = tasksResult[1];
        bool airlineExists = tasksResult[2];
        bool aircraftExists = tasksResult[3];

        /*
        //check flight existance
        if (flightExists == true)
        {
            throw new FlightAlreadyExistsException($"Flight with ID {flightAddRequest.FlightID} already exists.");
        }
        */
        //check airports codes are different
        if (String.Equals(
                flightAddRequest.DepartureAirportCode,
                flightAddRequest.ArrivalAirportCode,
                StringComparison.OrdinalIgnoreCase) == true
           )
        {
            throw new InvalidAirportCodeException("Departure and arrival airport codes cannot be the same.");
        }

        //check departure airport existance
        if (departureAirportExists == false)
        {
            throw new AirportNotFoundException($"Departure airport with code {flightAddRequest.DepartureAirportCode} does not exist.");
        }

        //check arrival airport existance
        if (arrivalAirportExists == false)
        {
            throw new AirportNotFoundException($"Arrival airport with code {flightAddRequest.ArrivalAirportCode} does not exist.");
        }

        //check airline existance

        if (airlineExists == false)
        {
            throw new AirlineNotFoundException($"Airline with ID {flightAddRequest.AirlineId} does not exist.");
        }

        //check arrival airport existance
        if (aircraftExists == false)
        {
            throw new AircraftNotFoundException($"Aircraft with ID {flightAddRequest.AircraftId} does not exist.");
        }

        //airline provided must be the same in aircraft
        Guid? associatedAirlineId = await _aircraftsGetterService.GetAirlineIdForAircraftAsync(flightAddRequest.AircraftId);

        if (associatedAirlineId == null)
        {
            throw new AirlineNotFoundException("Cannot find the associated airline id for this aircraft");
        }

        if (associatedAirlineId != flightAddRequest.AirlineId)
        {
            throw new AircraftNotFoundException($"Airline provided and actual airline for the aircraft are not the same.");
        }

        return true;
    }
}

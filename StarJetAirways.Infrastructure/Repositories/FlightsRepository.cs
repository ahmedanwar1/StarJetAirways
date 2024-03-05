using Neo4jClient;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Enums;
using StarJetAirways.Core.Utils;

namespace StarJetAirways.Infrastructure.Repositories;

public class FlightsRepository : IFlightsRepository
{
    private readonly IGraphClient _graphClient;

    public FlightsRepository(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task<Flight> AddFlightAsync(Flight flight, Guid airlineId, Guid aircraftId, string departureAirportCode, string arrivalAirportCode)
    {
        //create flight
        await _graphClient.Cypher
            .Create("(f:Flight $flight)")
            .WithParam("flight", flight)
            .ExecuteWithoutResultsAsync();

        //add relationships
        await _graphClient.Cypher
            .Match("(f: Flight)", "(al:Airline)", "(ac:Aircraft)", "(dep:Airport)", "(arr:Airport)")
            .Where((Flight f) => f.FlightID == flight.FlightID)
            .AndWhere((Airline al) => al.AirlineId == airlineId)
            .AndWhere((Aircraft ac) => ac.AircraftID == aircraftId)
            .AndWhere((Airport dep) => dep.AirportCode.Equals(departureAirportCode, StringComparison.OrdinalIgnoreCase) == true)
            .AndWhere((Airport arr) => arr.AirportCode.Equals(arrivalAirportCode, StringComparison.OrdinalIgnoreCase) == true)
            .Create("(f)-[:DEPARTURE]->(dep)," +
                    " (f)-[:ARRIVAL]->(arr)," +
                    " (f)-[:ASSIGNED_TO]->(ac)," +
                    " (f)<-[:OPERATES]-(al)")
            .ExecuteWithoutResultsAsync();

        return flight;
    }

    public async Task<FlightResponseDTO?> GetFlightByIdAsync(Guid flightId)
    {
        var flight = await _graphClient.Cypher
            .Match("(f: Flight)")
            .Where((Flight f) => f.FlightID == flightId)
            .Return(f => f.As<FlightResponseDTO>())
            .Limit(1)
            .ResultsAsync;

        return flight.FirstOrDefault();
    }

    public async Task<IEnumerable<FlightResponseDTO>?> GetDirectOneWayFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var flights = await _graphClient.Cypher
        .Match("(source:Airport {airportCode: $sourceAirportCode})-[:DEPARTURE]->(flight:Flight)-[:ARRIVAL]->(destination:Airport {airportCode: $destinationAirportCode})")
        .Where((Flight f) => f.DepartureDateTime.Date == searchCriteria.DepartureDate.Value.Date)
        .AndWhere((Flight f) => SeatsClassAvailability.ValidateSeatsClassAvailability(f, searchCriteria))
        .WithParams(new
        {
            sourceAirportCode = searchCriteria.DepartureAirportCode.ToUpper(),
            destinationAirportCode = searchCriteria.ArrivalAirportCode.ToUpper()
        })
        .Return(f => f.As<FlightResponseDTO>())
        .ResultsAsync;

        return flights;
    }

    public async Task<IEnumerable<FlightResponseDTO>?> GetDirectRoundTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var outboundFlights = await GetDirectOneWayFlightsAsync(new FlightSearchCriteriaDTO
        {
            DepartureAirportCode = searchCriteria.DepartureAirportCode,
            ArrivalAirportCode = searchCriteria.ArrivalAirportCode,
            DepartureDate = searchCriteria.DepartureDate,
            DirectFlightOnly = searchCriteria.DirectFlightOnly
        });

        var returnFlights = await GetDirectOneWayFlightsAsync(new FlightSearchCriteriaDTO
        {
            DepartureAirportCode = searchCriteria.ArrivalAirportCode,
            ArrivalAirportCode = searchCriteria.DepartureAirportCode,
            DepartureDate = searchCriteria.ReturnDate,
            DirectFlightOnly = searchCriteria.DirectFlightOnly
        });

        // Combine outbound and return flights for round trip
        var roundTripFlights = outboundFlights.Concat(returnFlights);

        return roundTripFlights;
    }

    public async Task<IEnumerable<FlightResponseDTO>?> GetOneWayTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var flights = await _graphClient.Cypher
        .Match("(source:Airport {airportCode: $sourceAirportCode})-[:DEPARTURE]->(flight:Flight)-[:ARRIVAL]->(destination:Airport {airportCode: $destinationAirportCode})")
        .Where((Flight f) => f.DepartureDateTime.Date == searchCriteria.DepartureDate.Value.Date)
        .AndWhere((Flight f) => SeatsClassAvailability.ValidateSeatsClassAvailability(f, searchCriteria))
        .WithParams(new
        {
            sourceAirportCode = searchCriteria.DepartureAirportCode.ToUpper(),
            destinationAirportCode = searchCriteria.ArrivalAirportCode.ToUpper()
        })
        .OptionalMatch("(flight)-[:DEPARTS_FROM*1..3]->(transit:Airport)")
        .Return(f => f.As<FlightResponseDTO>())
        .ResultsAsync;

        return flights;
    }

    public async Task<IEnumerable<FlightResponseDTO>?> GetRoundTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var outboundFlights = await GetOneWayTripFlightsAsync(new FlightSearchCriteriaDTO
        {
            DepartureAirportCode = searchCriteria.DepartureAirportCode,
            ArrivalAirportCode = searchCriteria.ArrivalAirportCode,
            DepartureDate = searchCriteria.DepartureDate,
            DirectFlightOnly = searchCriteria.DirectFlightOnly
        });

        var returnFlights = await GetOneWayTripFlightsAsync(new FlightSearchCriteriaDTO
        {
            DepartureAirportCode = searchCriteria.ArrivalAirportCode,
            ArrivalAirportCode = searchCriteria.DepartureAirportCode,
            DepartureDate = searchCriteria.ReturnDate,
            DirectFlightOnly = searchCriteria.DirectFlightOnly
        });

        // Combine outbound and return flights for round trip
        var roundTripFlights = outboundFlights.Concat(returnFlights);

        return roundTripFlights;
    }

    public async Task<IEnumerable<FlightResponseDTO>?> GetTripWithAllDatesFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var flights = await _graphClient.Cypher
        .Match("(source:Airport {airportCode: $sourceAirportCode})-[:DEPARTURE]->(flight:Flight)-[:ARRIVAL]->(destination:Airport {airportCode: $destinationAirportCode})")
        .Where((Flight f) => SeatsClassAvailability.ValidateSeatsClassAvailability(f, searchCriteria))
        .WithParams(new
        {
            sourceAirportCode = searchCriteria.DepartureAirportCode.ToUpper(),
            destinationAirportCode = searchCriteria.ArrivalAirportCode.ToUpper()
        })
        .OptionalMatch("(flight)-[:DEPARTS_FROM*0..3]->(transit:Airport)")
        .Return(f => f.As<FlightResponseDTO>())
        .ResultsAsync;

        return flights;
    }
}

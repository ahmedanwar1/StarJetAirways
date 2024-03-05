using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IFlightsRepository
{
    public Task<Flight> AddFlightAsync(Flight flight, Guid airlineId, Guid aircraftId, string departureAirportCode, string arrivalAirportCode);

    public Task<FlightResponseDTO?> GetFlightByIdAsync(Guid flightId);

    public Task<IEnumerable<FlightResponseDTO>?> GetRoundTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria);
    public Task<IEnumerable<FlightResponseDTO>?> GetOneWayTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria);
    public Task<IEnumerable<FlightResponseDTO>?> GetDirectRoundTripFlightsAsync(FlightSearchCriteriaDTO searchCriteria);
    public Task<IEnumerable<FlightResponseDTO>?> GetDirectOneWayFlightsAsync(FlightSearchCriteriaDTO searchCriteria);
    public Task<IEnumerable<FlightResponseDTO>?> GetTripWithAllDatesFlightsAsync(FlightSearchCriteriaDTO searchCriteria);

}

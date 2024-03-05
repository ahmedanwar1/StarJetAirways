using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Services.SearchFlights;

public class RoundTripStrategy : IFlightsSearchStrategy
{
    private readonly IFlightsRepository _flightsRepository;

    public RoundTripStrategy(IFlightsRepository flightsRepository)
    {
        _flightsRepository = flightsRepository;
    }

    public async Task<IEnumerable<FlightResponseDTO>?> SearchFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var flights = await _flightsRepository.GetRoundTripFlightsAsync(searchCriteria);

        return flights;
    }
}

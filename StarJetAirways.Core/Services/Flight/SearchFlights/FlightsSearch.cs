using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Services.SearchFlights;

public abstract class FlightsSearch
{
    protected readonly IFlightsRepository flightsRepository;

    public FlightsSearch(IFlightsRepository flightsRepository)
    {
        this.flightsRepository = flightsRepository;
    }

    protected abstract IFlightsSearchStrategy? CreateFlightsSearchStrategy(FlightSearchCriteriaDTO searchCriteria);

    public async Task<IEnumerable<FlightResponseDTO>?> SearchFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        IFlightsSearchStrategy? searchStrategy = CreateFlightsSearchStrategy(searchCriteria);

        if (searchStrategy == null)
        {
            throw new ArgumentException("Invalid search criteria.");
        }

        var flights = await searchStrategy.SearchFlightsAsync(searchCriteria);

        return flights;
    }
}

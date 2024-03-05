using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;
using StarJetAirways.Core.Services.SearchFlights;

namespace StarJetAirways.Core.Services;

public class FlightsGetterService : IFlightsGetterService
{
    private readonly IFlightsRepository _flightsRepository;
    private readonly FlightsSearch _flightsSearch;

    public FlightsGetterService(IFlightsRepository flightsRepository, FlightsSearch flightsSearch)
    {
        _flightsRepository = flightsRepository;
        _flightsSearch = flightsSearch;
    }

    public Task<IEnumerable<FlightResponseDTO>?> SearchFlightsAsync(FlightSearchCriteriaDTO searchCriteria)
    {
        var flights = _flightsSearch.SearchFlightsAsync(searchCriteria);

        return flights;
    }
}

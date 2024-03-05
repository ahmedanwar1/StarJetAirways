using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IFlightsGetterService
{
    public Task<IEnumerable<FlightResponseDTO>?> SearchFlightsAsync(FlightSearchCriteriaDTO searchCriteria);
}

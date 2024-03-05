
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IFlightsAdderService
{
    public Task<FlightResponseDTO> AddFlightAsync(FlightAddRequestDTO flightAddRequest);
}

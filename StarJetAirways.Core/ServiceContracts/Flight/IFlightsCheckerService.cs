using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IFlightsCheckerService
{
    public Task<bool> CheckFlightExistsAsync(Guid flightId);
    public Task<bool> IsFlightValidForAddingAsync(FlightAddRequestDTO flightAddRequest);
}

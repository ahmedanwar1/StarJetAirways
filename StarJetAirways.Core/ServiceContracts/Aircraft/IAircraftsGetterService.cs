using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAircraftsGetterService
{
    public Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirlineAsync();
    public Task<IEnumerable<AircraftResponseDTO>> GetAircraftsAsync();
    public Task<AircraftResponseDTO?> GetAircraftByIdAsync(Guid aircraftId);
    public Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineByIdAsync(Guid aircraftId);
    public Task<Guid?> GetAirlineIdForAircraftAsync(Guid aircraftId);
}

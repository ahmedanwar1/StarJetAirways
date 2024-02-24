using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAircraftsGetterService
{
    public Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirline();
    public Task<IEnumerable<AircraftResponseDTO>> GetAircrafts();
    public Task<AircraftResponseDTO?> GetAircraftById(Guid id);
    public Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineById(Guid id);
}

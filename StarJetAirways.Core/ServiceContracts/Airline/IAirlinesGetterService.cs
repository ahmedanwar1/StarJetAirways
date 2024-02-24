using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirlinesGetterService
{
    public Task<IEnumerable<AirlineResponseDTO>> GetAllAirlines();
    public Task<AirlineResponseDTO?> GetAirlineById(Guid id);
    public Task<bool> CheckAirlineExistsAsync(Guid id);
}

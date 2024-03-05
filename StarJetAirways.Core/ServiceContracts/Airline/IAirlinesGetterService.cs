using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirlinesGetterService
{
    public Task<IEnumerable<AirlineResponseDTO>> GetAllAirlinesAsync();
    public Task<AirlineResponseDTO?> GetAirlineByIdAsync(Guid id);
}

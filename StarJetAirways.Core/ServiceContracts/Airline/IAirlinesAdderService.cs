using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirlinesAdderService
{
    public Task<AirlineResponseDTO> AddAirlineAsync(AirlineAddRequestDTO airlineAddRequest);
}

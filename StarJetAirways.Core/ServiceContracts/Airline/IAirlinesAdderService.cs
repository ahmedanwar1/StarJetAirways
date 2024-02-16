using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirlinesAdderService
{
    public Task<AirlineResponseDTO> AddAirline(AirlineAddRequestDTO airlineAddRequest);
}

using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirportsAdderService
{
    public Task<AirportResponseDTO> AddAirportAsync(AirportAddRequestDTO airportAddRequest);
}

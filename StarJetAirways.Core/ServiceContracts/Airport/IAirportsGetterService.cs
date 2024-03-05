using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirportsGetterService
{
    public Task<IEnumerable<AirportResponseDTO>> GetAllAirportsAsync();
    public Task<AirportResponseDTO?> GetAirportByCodeAsync(string airportCode);
}

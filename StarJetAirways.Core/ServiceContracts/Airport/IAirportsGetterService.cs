using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAirportsGetterService
{
    public Task<IEnumerable<AirportResponseDTO>> GetAllAirports();
    public Task<AirportResponseDTO> GetAirportByCode(string airportCode);
}

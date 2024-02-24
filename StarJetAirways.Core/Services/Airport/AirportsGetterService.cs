using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirportsGetterService : IAirportsGetterService
{
    private readonly IAirportsRepository _airportsRepository;

    public AirportsGetterService(IAirportsRepository airportsRepository)
    {
        _airportsRepository = airportsRepository;
    }

    public async Task<IEnumerable<AirportResponseDTO>> GetAllAirports()
    {
        IEnumerable<AirportResponseDTO> airports = await _airportsRepository.GetAirports();

        return airports;
    }

    public async Task<AirportResponseDTO?> GetAirportByCode(string airportCode)
    {
        AirportResponseDTO? airport = await _airportsRepository.GetAirportByCode(airportCode);

        return airport;
    }

    public async Task<bool> CheckAirportExistsAsync(string airportCode)
    {
        var airport = await GetAirportByCode(airportCode);

        if (airport == null)
        {
            return false;
        }

        return true;
    }
}

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

    public async Task<IEnumerable<AirportResponseDTO>> GetAllAirportsAsync()
    {
        IEnumerable<AirportResponseDTO> airports = await _airportsRepository.GetAirportsAsync();

        return airports;
    }

    public async Task<AirportResponseDTO?> GetAirportByCodeAsync(string airportCode)
    {
        AirportResponseDTO? airport = await _airportsRepository.GetAirportByCodeAsync(airportCode);

        return airport;
    }

}

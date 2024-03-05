using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirportsCheckerService : IAirportsCheckerService
{
    private readonly IAirportsRepository _airportsRepository;

    public AirportsCheckerService(IAirportsRepository airportsRepository)
    {
        _airportsRepository = airportsRepository;
    }

    public async Task<bool> CheckAirportExistsAsync(string airportCode)
    {
        var airport = await _airportsRepository.GetAirportByCodeAsync(airportCode);

        if (airport == null)
        {
            return false;
        }

        return true;
    }
}

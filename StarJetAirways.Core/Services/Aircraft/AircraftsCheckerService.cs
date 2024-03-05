using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AircraftsCheckerService : IAircraftsCheckerService
{
    private readonly IAircraftsRepository _aircraftsRepository;

    public AircraftsCheckerService(IAircraftsRepository aircraftsRepository)
    {
        _aircraftsRepository = aircraftsRepository;
    }

    public async Task<bool> CheckAircraftExistsAsync(Guid aircraftId)
    {
        var aircraft = await _aircraftsRepository.GetAircraftByIdAsync(aircraftId);

        if (aircraft == null)
        {
            return false;
        }

        return true;
    }
}

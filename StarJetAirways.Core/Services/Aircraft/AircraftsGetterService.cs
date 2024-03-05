using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AircraftsGetterService : IAircraftsGetterService
{
    private readonly IAircraftsRepository _aircraftsRepository;

    public AircraftsGetterService(IAircraftsRepository aircraftsRepository)
    {
        _aircraftsRepository = aircraftsRepository;
    }

    public async Task<IEnumerable<AircraftResponseDTO>> GetAircraftsAsync()
    {
        IEnumerable<AircraftResponseDTO> aircrafts = await _aircraftsRepository.GetAircraftsAsync();

        return aircrafts;
    }

    public async Task<AircraftResponseDTO?> GetAircraftByIdAsync(Guid id)
    {
        AircraftResponseDTO? aircraft = await _aircraftsRepository.GetAircraftByIdAsync(id);

        return aircraft;
    }

    public async Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirlineAsync()
    {
        IEnumerable<AircraftWithAirlineResponseDTO> aircrafts = await _aircraftsRepository.GetAircraftsWithAirlineAsync();

        return aircrafts;
    }

    public async Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineByIdAsync(Guid id)
    {
        AircraftWithAirlineResponseDTO? aircraft = await _aircraftsRepository.GetAircraftWithAirlineByIdAsync(id);

        return aircraft;
    }

    public async Task<Guid?> GetAirlineIdForAircraftAsync(Guid aircraftId)
    {
        Guid? airlineId = await _aircraftsRepository.GetAirlineIdForAircraftAsync(aircraftId);

        return airlineId;
    }
}

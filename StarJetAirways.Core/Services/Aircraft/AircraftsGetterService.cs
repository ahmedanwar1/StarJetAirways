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

    public async Task<IEnumerable<AircraftResponseDTO>> GetAircrafts()
    {
        IEnumerable<AircraftResponseDTO> aircrafts = await _aircraftsRepository.GetAircrafts();

        return aircrafts;
    }

    public async Task<AircraftResponseDTO?> GetAircraftById(Guid id)
    {
        AircraftResponseDTO? aircraft = await _aircraftsRepository.GetAircraftById(id);

        return aircraft;
    }

    public async Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirline()
    {
        IEnumerable<AircraftWithAirlineResponseDTO> aircrafts = await _aircraftsRepository.GetAircraftsWithAirline();

        return aircrafts;
    }

    public async Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineById(Guid id)
    {
        AircraftWithAirlineResponseDTO? aircraft = await _aircraftsRepository.GetAircraftWithAirlineById(id);

        return aircraft;
    }
}

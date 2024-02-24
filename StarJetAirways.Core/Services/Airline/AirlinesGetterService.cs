using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirlinesGetterService : IAirlinesGetterService
{
    private readonly IAirlinesRepository _airlinesRepository;
    public AirlinesGetterService(IAirlinesRepository airlinesRepository)
    {
        _airlinesRepository = airlinesRepository;
    }

    public async Task<IEnumerable<AirlineResponseDTO>> GetAllAirlines()
    {
        var airlines = await _airlinesRepository.GetAirlines();

        return airlines;
    }

    public async Task<AirlineResponseDTO?> GetAirlineById(Guid id)
    {
        var airline = await _airlinesRepository.GetAirlineById(id);

        return airline;
    }

    public async Task<bool> CheckAirlineExistsAsync(Guid id)
    {
        var airline = await _airlinesRepository.GetAirlineById(id);

        if (airline == null)
        {
            return false;
        }

        return true;
    }
}

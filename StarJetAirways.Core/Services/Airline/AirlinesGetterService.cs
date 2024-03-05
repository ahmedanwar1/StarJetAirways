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

    public async Task<IEnumerable<AirlineResponseDTO>> GetAllAirlinesAsync()
    {
        var airlines = await _airlinesRepository.GetAirlinesAsync();

        return airlines;
    }

    public async Task<AirlineResponseDTO?> GetAirlineByIdAsync(Guid id)
    {
        var airline = await _airlinesRepository.GetAirlineByIdAsync(id);

        return airline;
    }

}

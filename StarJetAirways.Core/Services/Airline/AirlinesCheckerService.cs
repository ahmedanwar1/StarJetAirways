using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.Core.Services;

public class AirlinesCheckerService : IAirlinesCheckerService
{
    private readonly IAirlinesRepository _airlinesRepository;

    public AirlinesCheckerService(IAirlinesRepository airlinesRepository)
    {
        _airlinesRepository = airlinesRepository;
    }

    public async Task<bool> CheckAirlineExistsAsync(Guid id)
    {
        var airline = await _airlinesRepository.GetAirlineByIdAsync(id);

        if (airline == null)
        {
            return false;
        }

        return true;
    }
}

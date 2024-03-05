using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAirlinesRepository
{
    public Task<IEnumerable<AirlineResponseDTO>> GetAirlinesAsync();

    public Task<AirlineResponseDTO?> GetAirlineByIdAsync(Guid id);

    public Task<Airline> AddAirlineAsync(Airline airline);

}

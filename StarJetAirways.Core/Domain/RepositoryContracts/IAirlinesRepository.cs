using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAirlinesRepository
{
    public Task<IEnumerable<AirlineResponseDTO>> GetAirlines();

    public Task<AirlineResponseDTO?> GetAirlineById(Guid id);

    public Task<Airline> AddAirline(Airline airline);

}

using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAircraftsRepository
{
    public Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirlineAsync();
    public Task<IEnumerable<AircraftResponseDTO>> GetAircraftsAsync();
    public Task<AircraftResponseDTO?> GetAircraftByIdAsync(Guid id);
    public Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineByIdAsync(Guid id);
    public Task<Aircraft> AddAircraftAsync(Aircraft aircraft, Guid airlineId);
    public Task<Guid?> GetAirlineIdForAircraftAsync(Guid aircraftId);
}

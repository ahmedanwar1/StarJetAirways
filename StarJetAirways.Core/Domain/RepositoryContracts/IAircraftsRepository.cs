using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAircraftsRepository
{
    public Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirline();
    public Task<IEnumerable<AircraftResponseDTO>> GetAircrafts();
    public Task<AircraftResponseDTO?> GetAircraftById(Guid id);
    public Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineById(Guid id);
    public Task<Aircraft> AddAircraft(Aircraft aircraft, Guid airlineId);
}

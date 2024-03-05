using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAirportsRepository
{
    public Task<IEnumerable<AirportResponseDTO>> GetAirportsAsync();
    public Task<AirportResponseDTO?> GetAirportByCodeAsync(string airportCode);
    public Task<Airport> AddAirportAsync(Airport airport);
}

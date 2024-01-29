using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Domain.RepositoryContracts;

public interface IAirportsRepository
{
    public Task<IEnumerable<AirportResponseDTO>> GetAirports();
    public Task<AirportResponseDTO> GetAirportByCode(string airportCode);
    public Task<Airport> AddAirport(Airport airport);
    public Task<bool> CheckAirportExistsAsync(string airportCode);
}

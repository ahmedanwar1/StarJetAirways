using Neo4jClient;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.Models.Entities;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Infrastructure.Repositories;

public class AirportsRepository : IAirportsRepository
{
    private readonly IGraphClient _graphClient;

    public AirportsRepository(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task<IEnumerable<AirportResponseDTO>> GetAirportsAsync()
    {
        var airports = await _graphClient.Cypher.Match("(a:Airport)")
                .Return(a => a.As<AirportResponseDTO>()).ResultsAsync;

        return airports;
    }

    public async Task<AirportResponseDTO?> GetAirportByCodeAsync(string airportCode)
    {
        var airport = await _graphClient.Cypher.Match("(a:Airport)")
                .Where((Airport a) => a.AirportCode == airportCode)
                .Return(a => a.As<AirportResponseDTO>()).Limit(1).ResultsAsync;

        return airport.FirstOrDefault();
    }

    public async Task<Airport> AddAirportAsync(Airport airport)
    {

        await _graphClient.Cypher
            .Merge("(airport:Airport { airportCode: $airportCode })")
            .OnCreate()
            .Set("airport = $newAirport")
            .WithParams(new
            {
                airportCode = airport.AirportCode,
                newAirport = airport
            })
            .ExecuteWithoutResultsAsync();


        return airport;
    }

}

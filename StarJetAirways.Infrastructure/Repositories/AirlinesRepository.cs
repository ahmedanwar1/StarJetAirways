﻿using Neo4jClient;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Infrastructure.Repositories;

public class AirlinesRepository : IAirlinesRepository
{
    private readonly IGraphClient _graphClient;

    public AirlinesRepository(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task<IEnumerable<AirlineResponseDTO>> GetAirlines()
    {
        var airlines = await _graphClient.Cypher.Match("(a:Airline)")
            .Return(a => a.As<AirlineResponseDTO>()).ResultsAsync;

        return airlines;
    }

    public async Task<AirlineResponseDTO> GetAirlineById(Guid id)
    {
        var airline = await _graphClient.Cypher.Match("(a: Airline)")
            .Where((Airline a) => a.AirlineId == id)
                .Return(a => a.As<AirlineResponseDTO>()).Limit(1).ResultsAsync;

        return airline.FirstOrDefault();
    }

    public async Task<Airline> AddAirline(Airline airline)
    {
        await _graphClient.Cypher
           .Merge("(airline:Airline { airlineId: $id })")
           .OnCreate()
           .Set("airline = $newAirline")
           .WithParams(new
           {
               id = airline.AirlineId,
               newAirline = airline
           })
           .ExecuteWithoutResultsAsync();


        return airline;
    }

    public async Task<bool> CheckAirlineExistsAsync(Guid id)
    {
        var airline = await GetAirlineById(id);

        if (airline == null)
        {
            return false;
        }

        return true;
    }
}

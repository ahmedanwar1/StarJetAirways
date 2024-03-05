using Neo4jClient;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.Models.Entities;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Infrastructure.Repositories;

public class AircraftsRepository : IAircraftsRepository
{
    private readonly IGraphClient _graphClient;

    public AircraftsRepository(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task<IEnumerable<AircraftResponseDTO>> GetAircraftsAsync()
    {
        var response = await _graphClient.Cypher.Match("(aircraft:Aircraft)")
            .Return((aircraft) => aircraft.As<AircraftResponseDTO>()).ResultsAsync;

        return response;
    }

    public async Task<AircraftResponseDTO?> GetAircraftByIdAsync(Guid id)
    {
        var response = await _graphClient.Cypher.Match("(aircraft: Aircraft)")
            .Where((Aircraft aircraft) => aircraft.AircraftID == id)
            .Return((aircraft) => aircraft.As<AircraftResponseDTO>())
            .Limit(1)
            .ResultsAsync;

        return response.FirstOrDefault();
    }

    public async Task<IEnumerable<AircraftWithAirlineResponseDTO>> GetAircraftsWithAirlineAsync()
    {
        var response = await _graphClient.Cypher.Match("(airline:Airline)-[:OPERATES]->(aircraft:Aircraft)")
            .Return((airline, aircraft) => new
            {
                Aircraft = aircraft.As<AircraftResponseDTO>(),
                Airline = airline.As<AirlineResponseDTO>(),
            }).ResultsAsync;

        IEnumerable<AircraftWithAirlineResponseDTO> aircraftsWithAirlineResponse = response.Select(item =>
            {
                return new AircraftWithAirlineResponseDTO()
                {
                    AircraftID = item.Aircraft.AircraftID,
                    AircraftType = item.Aircraft.AircraftType,
                    TotalBusinessClassSeats = item.Aircraft.TotalBusinessClassSeats,
                    TotalEconomyClassSeats = item.Aircraft.TotalEconomyClassSeats,
                    TotalFirstClassSeats = item.Aircraft.TotalFirstClassSeats,
                    Airline = item.Airline
                };
            }
        );

        return aircraftsWithAirlineResponse;
    }

    public async Task<AircraftWithAirlineResponseDTO?> GetAircraftWithAirlineByIdAsync(Guid id)
    {
        var response = await _graphClient.Cypher.Match("(airline:Airline)-[:OPERATES]->(aircraft:Aircraft)")
             .Where((Aircraft aircraft) => aircraft.AircraftID == id)
             .Return((airline, aircraft) => new
             {
                 Aircraft = aircraft.As<AircraftResponseDTO>(),
                 Airline = airline.As<AirlineResponseDTO>(),
             })
            .Limit(1)
            .ResultsAsync;

        var aircraftResult = response.FirstOrDefault();

        if (aircraftResult == null)
        {
            return null;
        }

        AircraftWithAirlineResponseDTO aircraftWithAirlineResponse = new AircraftWithAirlineResponseDTO()
        {
            AircraftID = aircraftResult.Aircraft.AircraftID,
            AircraftType = aircraftResult.Aircraft.AircraftType,
            Airline = aircraftResult.Airline,
            TotalBusinessClassSeats = aircraftResult.Aircraft.TotalBusinessClassSeats,
            TotalEconomyClassSeats = aircraftResult.Aircraft.TotalEconomyClassSeats,
            TotalFirstClassSeats = aircraftResult.Aircraft.TotalFirstClassSeats
        };



        return aircraftWithAirlineResponse;
    }

    public async Task<Aircraft> AddAircraftAsync(Aircraft aircraft, Guid airlineId)
    {

        await _graphClient.Cypher
        .Match("(airline:Airline)")
        .Where((Airline airline) => airline.AirlineId == airlineId)
        .Create("(airline)-[:OPERATES]->(aircraft:Aircraft $newAircraft)")
        .WithParam("newAircraft", aircraft)
        .ExecuteWithoutResultsAsync();

        return aircraft;
    }

    public async Task<Guid?> GetAirlineIdForAircraftAsync(Guid aircraftId)
    {
        var airlineResponse = await _graphClient.Cypher.Match("(airline:Airline)-[:OPERATES]->(aircraft:Aircraft)")
            .Return((airline) => airline.As<AirlineResponseDTO>())
            .ResultsAsync;

        return airlineResponse.FirstOrDefault()?.AirlineId;
    }
}

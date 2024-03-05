using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Services.SearchFlights;

public class GeneralFlightsSearch : FlightsSearch
{
    public GeneralFlightsSearch(IFlightsRepository flightsRepository) : base(flightsRepository)
    {
    }

    protected override IFlightsSearchStrategy? CreateFlightsSearchStrategy(FlightSearchCriteriaDTO searchCriteria)
    {

        if (TripTypeEvaluator.IsRoundTrip(searchCriteria) == true)
        {
            return new RoundTripStrategy(flightsRepository);
        }
        else if (TripTypeEvaluator.IsOneWayTrip(searchCriteria) == true)
        {
            return new OneWayTripStrategy(flightsRepository);
        }
        else if (TripTypeEvaluator.IsDirectRoundTrip(searchCriteria) == true)
        {
            return new DirectRoundTripStrategy(flightsRepository);
        }
        else if (TripTypeEvaluator.IsDirectOneWay(searchCriteria) == true)
        {
            return new DirectOneWayStrategy(flightsRepository);
        }
        else if (TripTypeEvaluator.IsTripWithAllDates(searchCriteria) == true)
        {
            return new TripWithAllDatesStrategy(flightsRepository);
        }

        return null;
    }

}

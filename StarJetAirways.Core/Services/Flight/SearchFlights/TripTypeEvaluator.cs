using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.Services.SearchFlights;

public static class TripTypeEvaluator
{
    public static bool IsRoundTrip(FlightSearchCriteriaDTO searchCriteria)
    {
        return searchCriteria.DepartureDate != null &&
            searchCriteria.ReturnDate != null &&
            searchCriteria.DirectFlightOnly == false;
    }

    public static bool IsOneWayTrip(FlightSearchCriteriaDTO searchCriteria)
    {
        return searchCriteria.DepartureDate != null &&
            searchCriteria.ReturnDate == null &&
            searchCriteria.DirectFlightOnly == false;
    }

    public static bool IsDirectRoundTrip(FlightSearchCriteriaDTO searchCriteria)
    {
        return searchCriteria.DepartureDate != null &&
            searchCriteria.ReturnDate != null &&
            searchCriteria.DirectFlightOnly == true;
    }

    public static bool IsDirectOneWay(FlightSearchCriteriaDTO searchCriteria)
    {
        return searchCriteria.DepartureDate != null &&
            searchCriteria.ReturnDate == null &&
            searchCriteria.DirectFlightOnly == true;
    }

    public static bool IsTripWithAllDates(FlightSearchCriteriaDTO searchCriteria)
    {
        return searchCriteria.DepartureDate == null && searchCriteria.ReturnDate == null;
    }

}

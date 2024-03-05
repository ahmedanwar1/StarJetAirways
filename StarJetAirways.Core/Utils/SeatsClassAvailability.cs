using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Enums;

namespace StarJetAirways.Core.Utils;

public static class SeatsClassAvailability
{
    public static bool ValidateSeatsClassAvailability(Flight f, FlightSearchCriteriaDTO searchCriteria)
    {
        bool valid;

        switch (searchCriteria.TravelClass)
        {
            case TravelClassEnum.FirstClass:
                valid = f.AvailableFirstClassSeats >= searchCriteria.PassengerCount;
                break;
            case TravelClassEnum.BusinessClass:
                valid = f.AvailableBusinessClassSeats >= searchCriteria.PassengerCount;
                break;
            case TravelClassEnum.EconomyClass:
                valid = f.AvailableEconomyClassSeats >= searchCriteria.PassengerCount;
                break;
            default:
                valid = true;
                break;
        }

        return valid;
    }
}

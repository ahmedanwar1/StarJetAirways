namespace StarJetAirways.Core.ServiceContracts;

public interface IAirportsCheckerService
{
    public Task<bool> CheckAirportExistsAsync(string airportCode);
}

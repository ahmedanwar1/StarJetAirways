namespace StarJetAirways.Core.ServiceContracts;

public interface IAircraftsCheckerService
{
    public Task<bool> CheckAircraftExistsAsync(Guid aircraftId);

}

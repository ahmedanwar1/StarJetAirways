namespace StarJetAirways.Core.ServiceContracts;

public interface IAirlinesCheckerService
{
    public Task<bool> CheckAirlineExistsAsync(Guid id);
}

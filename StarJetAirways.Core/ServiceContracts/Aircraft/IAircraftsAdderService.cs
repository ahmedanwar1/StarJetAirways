using StarJetAirways.Core.DTOs;

namespace StarJetAirways.Core.ServiceContracts;

public interface IAircraftsAdderService
{
    public Task<AircraftResponseDTO> AddAircraft(AircraftAddRequestDTO aircraftAddRequest);
}

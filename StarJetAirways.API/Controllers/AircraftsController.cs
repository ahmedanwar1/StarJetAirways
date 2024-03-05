using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarJetAirways.Core.Domain.Entities.Neo4j.Nodes;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Exceptions;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftsGetterService _aircraftsGetterService;
        private readonly IAircraftsAdderService _aircraftsAdderService;

        public AircraftsController(IAircraftsGetterService aircraftsGetterService, IAircraftsAdderService aircraftsAdderService)
        {
            _aircraftsGetterService = aircraftsGetterService;
            _aircraftsAdderService = aircraftsAdderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirlines()
        {
            try
            {
                IEnumerable<AircraftResponseDTO> aircrafts = await _aircraftsGetterService.GetAircraftsAsync();

                return Ok(aircrafts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAirlineById([FromRoute] Guid id)
        {
            try
            {
                AircraftResponseDTO? aircraft = await _aircraftsGetterService.GetAircraftByIdAsync(id);

                if (aircraft == null)
                {
                    return NotFound();
                }

                return Ok(aircraft);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("Aircrafts_With_Airline")]
        public async Task<IActionResult> GetAllAircraftsWithAirline()
        {
            try
            {
                IEnumerable<AircraftWithAirlineResponseDTO> aircraftsWithAirline = await _aircraftsGetterService.GetAircraftsWithAirlineAsync();

                return Ok(aircraftsWithAirline);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("Aircrafts_With_Airline/{id:guid}")]
        public async Task<IActionResult> GetAircraftWithAirlineById([FromRoute] Guid id)
        {
            try
            {
                AircraftWithAirlineResponseDTO? aircraftWithAirline = await _aircraftsGetterService.GetAircraftWithAirlineByIdAsync(id);

                if (aircraftWithAirline == null)
                {
                    return NotFound();
                }

                return Ok(aircraftWithAirline);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAircraft([FromBody] AircraftAddRequestDTO aircraftAddRequest)
        {
            try
            {
                var aircraft = await _aircraftsAdderService.AddAircraftAsync(aircraftAddRequest);

                return Created($"/aircraft/{aircraft.AircraftID}", aircraft);
            }
            catch (InvalidAirlineIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

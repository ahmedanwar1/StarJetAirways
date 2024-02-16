using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarJetAirways.API.CustomModelBinders;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.Exceptions;
using StarJetAirways.Core.ServiceContracts;
using System.Net;

namespace StarJetAirways.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportsGetterService _airportsGetterService;
        private readonly IAirportsAdderService _airportsAdderService;

        public AirportsController(
            IAirportsGetterService airportsGetterService,
            IAirportsAdderService airportsAdderService
        )
        {
            _airportsGetterService = airportsGetterService;
            _airportsAdderService = airportsAdderService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAirports()
        {
            var airports = await _airportsGetterService.GetAllAirports();

            return Ok(airports);
        }

        [HttpGet("{airportCode:alpha}")]
        public async Task<IActionResult> GetAirportByCode([FromRoute] string airportCode)
        {
            try
            {
                var airport = await _airportsGetterService.GetAirportByCode(airportCode.ToUpper());

                if (airport == null)
                {
                    return NotFound(new { status = 404, message = "Airport not found!" });
                }

                return Ok(airport);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAirport([FromBody] AirportAddRequestDTO airportAddRequest)
        {
            try
            {
                AirportResponseDTO airport = await _airportsAdderService.AddAirport(airportAddRequest);

                return Created($"/airport/{airport.AirportCode}", airport);
            }
            catch (InvalidAirportCodeException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

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
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsAdderService _flightsAdderService;
        private readonly IFlightsGetterService _flightsGetterService;

        public FlightsController(IFlightsAdderService flightsAdderService, IFlightsGetterService flightsGetterService)
        {
            _flightsAdderService = flightsAdderService;
            _flightsGetterService = flightsGetterService;
        }

        [HttpGet("/search")]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchCriteriaDTO searchCriteria)
        {
            try
            {
                var flights = await _flightsGetterService.SearchFlightsAsync(searchCriteria);

                if (flights == null)
                {
                    return NotFound();
                }

                return Ok(flights);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] FlightAddRequestDTO flightAddRequest)
        {
            try
            {
                var flight = await _flightsAdderService.AddFlightAsync(flightAddRequest);

                if (flight == null)
                {
                    return BadRequest("Faild to create flight");
                }

                return Created($"/flight/{flight.FlightID}", flight);

            }
            catch (Exception ex) when (
                ex is AircraftNotFoundException || ex is AirlineNotFoundException ||
                ex is AirportNotFoundException || ex is FlightAlreadyExistsException ||
                ex is InvalidAirlineIdException || ex is InvalidAirportCodeException
                )
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

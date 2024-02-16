using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarJetAirways.Core.DTOs;
using StarJetAirways.Core.ServiceContracts;

namespace StarJetAirways.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly IAirlinesGetterService _airlinesGetterService;
        private readonly IAirlinesAdderService _airlineAdderService;

        public AirlinesController(IAirlinesGetterService airlinesGetterService, IAirlinesAdderService airlineAdderService)
        {
            _airlinesGetterService = airlinesGetterService;
            _airlineAdderService = airlineAdderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirlines()
        {
            try
            {
                //fetch airlines
                var airlines = await _airlinesGetterService.GetAllAirlines();

                //return airlines
                return Ok(airlines);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAirline([FromRoute] Guid id)
        {
            try
            {
                var airline = await _airlinesGetterService.GetAirlineById(id);

                if (airline == null)
                {
                    return NotFound(new { status = 404, message = "Airline not found!" });
                }

                return Ok(airline);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirline(AirlineAddRequestDTO airlineAddRequest)
        {
            try
            {
                var createdAirline = await _airlineAdderService.AddAirline(airlineAddRequest);

                if (createdAirline == null)
                {
                    return BadRequest("Faild to create airline");
                }

                return Created($"/airline/{createdAirline.AirlineId}", createdAirline);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

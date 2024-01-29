using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.API.CustomModelBinders;

public class AirportRequestModelBinder : IModelBinder
{

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        //receive request body
        var request = bindingContext.HttpContext.Request;

        using (var requestBodyStreamReader = new StreamReader(request.Body))
        {
            var bodyValues = await requestBodyStreamReader.ReadToEndAsync();

            //Deserialize json to object AirportAddRequestDTO
            AirportAddRequestDTO? receivedAirportAddRequestDTO = JsonConvert.DeserializeObject<AirportAddRequestDTO>(bodyValues);

            if (receivedAirportAddRequestDTO == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            string airportCodeValue = receivedAirportAddRequestDTO.AirportCode.ToUpper();
            string airportNameValue = receivedAirportAddRequestDTO.AirportName;
            string cityValue = receivedAirportAddRequestDTO.City;
            string countryValue = receivedAirportAddRequestDTO.Country;

            AirportAddRequestDTO airportAddRequestDTO = new AirportAddRequestDTO
            {
                AirportCode = airportCodeValue,
                AirportName = airportNameValue,
                City = cityValue,
                Country = countryValue,
            };

            bindingContext.Result = ModelBindingResult.Success(airportAddRequestDTO);
            // return Task.CompletedTask;
        }
    }
}

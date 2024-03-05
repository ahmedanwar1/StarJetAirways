using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StarJetAirways.Core.DTOs;

namespace StarJetAirways.API.CustomModelBinders;

public class AirportRequestBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(AirportAddRequestDTO))
        {
            return new BinderTypeModelBinder(typeof(AirportRequestModelBinder));
        }

        return null;
    }
}

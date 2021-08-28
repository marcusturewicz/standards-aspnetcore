using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Standards.AspNetCore
{
    public class IsoDateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;

                var dateTimeParsed = DateTime.TryParseExact(valueAsString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeResult);
                if (dateTimeParsed)
                {
                    bindingContext.Result = ModelBindingResult.Success(dateTimeResult);
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid date; must be in ISO-8601 format i.e. YYYY-MM-DD.");
                }
            }

            return Task.CompletedTask;
        }      
    }

    public class IsoDateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime))
            {
                return new BinderTypeModelBinder(typeof(IsoDateModelBinder));
            }

            return null;
        }
    }      
}
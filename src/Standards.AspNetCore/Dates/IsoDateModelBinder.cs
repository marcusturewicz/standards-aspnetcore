using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Standards.AspNetCore
{
    /// <summary>
    /// An ASP.NET Core model binder to enforce ISO-8601 (YYYY-MM-DD) format for <see cref="System.DateTime"/> objects.
    /// <example>For example, apply to a Controller action:
    /// <code>
    /// [HttpGet]
    /// public IActionResult Get([ModelBinder(typeof(IsoDateModelBinder))] DateTime date) {
    ///     return Ok("Date is in ISO format!");
    /// }
    /// </code>
    /// </example>
    /// </summary>
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

    /// <summary>
    /// An ASP.NET Core model binder provider to enforce ISO-8601 (YYYY-MM-DD) format for <see cref="System.DateTime"/> objects.
    /// <example>For example, apply globally in ASP.NET Core Startup.cs:
    /// <code>
    /// public override void ConfigureServices(IServiceCollection services) {
    ///     services.AddControllers(options => {
    ///         options.ModelBinderProviders.Insert(0, new IsoDateModelBinderProvider());
    ///     })
    /// }
    /// </code>
    /// </example>
    /// </summary>
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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BO.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = valueResult.FirstValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(0m);
                return Task.CompletedTask;
            }

            // Normalize: replace comma with dot, then parse with InvariantCulture
            value = value.Replace(",", ".");

            // Handle thousand separators (e.g., 50.000.50 → 50000.50)
            var lastDot = value.LastIndexOf('.');
            if (lastDot > 0)
            {
                var beforeLast = value[..lastDot].Replace(".", "");
                value = beforeLast + "." + value[(lastDot + 1)..];
            }

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid number format.");
            }

            return Task.CompletedTask;
        }
    }

    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
            {
                return new DecimalModelBinder();
            }

            return null;
        }
    }
}
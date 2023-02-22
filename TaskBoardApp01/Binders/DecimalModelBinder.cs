using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskBoardApp01.Binders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && string.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal actualValue = 0m;
                bool succes = false;
                try
                {
                    string decValue = valueResult.FirstValue;
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    actualValue = Convert.ToDecimal(decValue, CultureInfo.CurrentCulture);
                    succes = true;
                }
                catch (FormatException e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if (succes)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }

            }
            return Task.CompletedTask;
        }
    }
}

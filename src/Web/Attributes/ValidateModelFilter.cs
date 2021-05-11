using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models.OperationResults;

namespace Web.Attributes
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                BadRequestObjectResult badRequestResult = new BadRequestObjectResult(context.ModelState);
                string serializedValidationResult = JsonSerializer.Serialize(badRequestResult.Value, jsonSerializerOptions);
                context.Result = new OkObjectResult(new OperationResult(OperationStatus.Error, serializedValidationResult));
            }
        }
    }
}
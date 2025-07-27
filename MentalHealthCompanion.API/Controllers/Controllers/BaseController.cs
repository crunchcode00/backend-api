using MentalHealthCompanion.Data.AppResponses;
using MentalHealthCompanion.Data.DTO.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MentalHealthCompanion.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private List<string> ValidationError(ModelStateDictionary modelState)
        {
            return modelState
                .Values
                .SelectMany(error => error.Errors)
                .Select(message => message.ErrorMessage)
                .ToList();
        }

        internal ApiResponse<bool> GenerateValidationErrorResponse(ModelStateDictionary modelState)
        {
            var error = ValidationError(modelState).FirstOrDefault();

            return new ApiResponse<bool>(ResponseCodes.ValiationError, error ?? "Model validation error");
        }
    }
}

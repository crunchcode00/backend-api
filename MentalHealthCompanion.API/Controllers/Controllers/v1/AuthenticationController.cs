using Asp.Versioning;
using MentalHealthCompanion.Data.DTO;
using MentalHealthCompanion.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthCompanion.API.Controllers.v1
{
    [ApiVersion("1")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(IAuthenService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid request body");
                return BadRequest(GenerateValidationErrorResponse(ModelState));
            }
            var result = await _authenticationService.LoginAsync(loginRequestDto, token);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingServerAPI.Modals;

namespace TrainingServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeyValidationController : ControllerBase
    {
        private readonly IApiKeyValidation _apiKeyValidation;

        // Constructor injection for IApiKeyValidation
        public ApiKeyValidationController(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }

        [HttpGet("header")]
        public IActionResult AuthenticateViaHeader()
        {
            // Retrieve the API Key from the header
            string? apiKey = Request.Headers["X-API-KEY"];
            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest("API key is missing.");

            // Validate the API key using the injected service
            bool isValid = _apiKeyValidation.IsValidApiKey(apiKey);

            if (!isValid)
                return Unauthorized("Invalid API key.");

            return Ok("API key is valid.");
        }
    }
}

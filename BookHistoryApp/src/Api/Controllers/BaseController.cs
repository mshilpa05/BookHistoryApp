using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleResponse<T>(T result)
        {
            if (result == null)
            {
                _logger.LogWarning("Requested resource not found.");
                return NotFound();
            }

            _logger.LogInformation("Request handled successfully.");
            return Ok(result);
        }

        protected IActionResult HandleCreationResponse<T>(T result, string locationUri, string successMessage = null)
        {
            if (result is null)
            {
                return BadRequest(new { Message = "Failed to create resource." });
            }

            return Created(locationUri, new
            {
                Message = successMessage ?? "Resource created successfully.",
                Data = result
            });
        }


        protected IActionResult HandleDeletionResponse(bool isDeleted)
        {
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound(new { Message = "Resource not found." });
        }

        protected IActionResult HandleUpdateResponse(bool isUpdated, string successMessage = null)
        {
            if (isUpdated)
            {
                return Ok(new { Message = successMessage ?? "Resource updated successfully." });
            }

            return NotFound(new { Message = "Resource not found." });
        }

        protected IActionResult HandleError(Exception exception)
        {
            _logger.LogError(exception, "An error occurred during the request.");
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}

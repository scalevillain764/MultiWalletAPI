using _interfaces;
using _result;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
namespace _base_controller
{
    [ApiController]
    public class BaseController: ControllerBase
    {
        protected IActionResult ProcessResult<T>(Result<T> rez) where T : class
        {
            if (!rez.IsSuccess)
            {
                return rez.errorType switch
                {
                    Result<T>.ErrorType.NotFound => NotFound(rez.ErrorMessage),
                    Result<T>.ErrorType.Validation => BadRequest(rez.ErrorMessage),
                    Result<T>.ErrorType.Forbidden => Forbid(),
                    Result<T>.ErrorType.Unauthorized => Unauthorized(rez.ErrorMessage),
                    Result<T>.ErrorType.Conflict => Conflict(rez.ErrorMessage)
                };
            }
            return Ok(rez.Data);
        }
        protected Ulid GetUserId() => (Ulid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId) ? userId : throw new UnauthorizedAccessException());
        protected Ulid ConvertStringToUlid(string value) => (Ulid.TryParse(value, out var valueUlid) ? valueUlid : throw new FormatException());
    }
}
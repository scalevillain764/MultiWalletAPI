using _interfaces;
using _result;
using Microsoft.AspNetCore.Mvc;
namespace _auth_controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /*private Task<Result<IActionResult>> { }
        [Route("login")]
        public async Task<Result>*/
    }
}
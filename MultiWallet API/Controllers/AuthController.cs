using _interfaces;
using _result;
using _user_registration_login_request_dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using _base_controller;
namespace _auth_controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service) => _service = service;

        [HttpPut]
        [Route("login")]
        public async Task<IActionResult> LogInAsync([FromBody] UserRegistrationandLogInRequestDTO DTO)
        {
            var rez = await _service.LogInAsync(DTO);
            return ProcessResult(rez);
        }

        [HttpPut]
        [Route("registration")]
        public async Task<IActionResult> RegistrAsync([FromBody] UserRegistrationandLogInRequestDTO DTO)
        {
            var rez = await _service.RegistrAsync(DTO);
            return ProcessResult(rez);
        }

        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var rez = await _service.RefreshAsync();
            return ProcessResult(rez);
        }
    }
}